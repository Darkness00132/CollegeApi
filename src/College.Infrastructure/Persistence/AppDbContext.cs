using College.Domain.Entities;
using College.Domain.Entities.Affairs;
using College.Domain.Entities.Attendence;
using College.Domain.Entities.Identity;
using College.Domain.Entities.LMS;
using College.Domain.Entities.Payment;
using College.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentEntity = College.Domain.Entities.Payment.Payment;

namespace College.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ==================== AFFAIRS ====================

        public DbSet<AcademicCollege> AcademicColleges { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Facility> Facilities { get; set; }

        public DbSet<CollegeProgram> CollegePrograms { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<ProfessorProfile> ProfessorProfiles { get; set; }

        public DbSet<AcademicAssistantProfile> AcademicAssistantProfiles { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<SemesterCourse> SemesterCourses { get; set; }

        public DbSet<SemesterCourseProfessor> SemesterCourseProfessors { get; set; }

        public DbSet<SemesterCourseAssistant> SemesterCourseAssistants { get; set; }

        public DbSet<SemesterCourseSchedule> SemesterCourseSchedules { get; set; }

        public DbSet<AcademicAdvisorAssignment> AcademicAdvisorAssignments { get; set; }

        public DbSet<SemesterRegistration> SemesterRegistrations { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<StudentComplaint> StudentComplaints { get; set; }

        public DbSet<StudentAffairsRequest> StudentAffairsRequests { get; set; }

        public DbSet<StudentCourseResult> StudentCourseResults { get; set; }

        public DbSet<StudentSemesterResult> StudentSemesterResults { get; set; }

        // ==================== ATTENDANCE ====================

        public DbSet<LectureSession> LectureSessions { get; set; }

        public DbSet<SectionSession> SectionSessions { get; set; }

        public DbSet<LectureAttendance> LectureAttendances { get; set; }

        public DbSet<SectionAttendance> SectionAttendances { get; set; }

        // ==================== IDENTITY ====================

        public DbSet<UserCollege> UserColleges { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // ==================== LMS ====================

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<CourseAnnouncement> CourseAnnouncements { get; set; }

        public DbSet<CourseMaterial> CourseMaterials { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<AssessmentResult> AssessmentResults { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<QuizQuestion> QuizQuestions { get; set; }

        public DbSet<QuizOption> QuizOptions { get; set; }

        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        // ==================== PAYMENT ====================

        public DbSet<PaymentEntity> Payments { get; set; }

        public DbSet<PaymentAttempt> PaymentAttempts { get; set; }

        // ==================== CROSS-CUTTING ====================

        public DbSet<AuditEvent> AuditEvents { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================== COMPOSITE PRIMARY KEYS ====================

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.SemesterRegistrationId, e.SemesterCourseId });

            modelBuilder.Entity<StudentCourseResult>()
                .HasKey(e => new { e.SemesterRegistrationId, e.SemesterCourseId });

            modelBuilder.Entity<SemesterCourseProfessor>()
                .HasKey(e => new { e.SemesterCourseId, e.ProfessorId });

            modelBuilder.Entity<SemesterCourseAssistant>()
                .HasKey(e => new { e.SemesterCourseId, e.AcademicAssistantProfileId });

            modelBuilder.Entity<UserCollege>()
                .HasKey(e => new { e.UserId, e.CollegeId });

            // ==================== REFRESH TOKENS ====================

            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.HasIndex(t => t.TokenHash).IsUnique();
                builder.HasIndex(t => t.UserId);

                builder.Property(t => t.TokenHash).HasMaxLength(64).IsRequired();
                builder.Property(t => t.ReplacedByTokenHash).HasMaxLength(64);

                builder.HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LectureAttendance>()
                .HasKey(e => new { e.LectureSessionId, e.StudentId });

            modelBuilder.Entity<SectionAttendance>()
                .HasKey(e => new { e.SectionSessionId, e.StudentId });

            modelBuilder.Entity<AssessmentResult>()
                .HasKey(e => new { e.AssessmentId, e.StudentId });

            modelBuilder.Entity<AssignmentSubmission>()
                .HasKey(e => new { e.AssignmentId, e.StudentId });

            modelBuilder.Entity<QuizAnswer>()
                .HasKey(e => new { e.QuizAttemptId, e.QuestionId });

            modelBuilder.Entity<ConversationParticipant>()
                .HasKey(e => new { e.ConversationId, e.UserId });

            // ==================== OWNED VALUE OBJECTS ====================

            modelBuilder.Entity<CollegeProgram>(builder =>
            {
                builder.OwnsOne(p => p.MaximumLevel, level =>
                {
                    level.Property(l => l.Value).HasColumnName("MaximumLevel");
                });

                builder.OwnsOne(p => p.BylawYear, year =>
                {
                    year.Ignore(y => y.EndYear);
                    year.Property(y => y.StartYear).HasColumnName("BylawYear_StartYear");
                });
            });

            modelBuilder.Entity<SemesterCourse>(builder =>
            {
                builder.OwnsOne(p => p.Level, level =>
                {
                    level.Property(l => l.Value).HasColumnName("Level");
                });
            });

            modelBuilder.Entity<Semester>(builder =>
            {
                builder.OwnsOne(p => p.AcademicYear, year =>
                {
                    year.Ignore(y => y.EndYear);
                    year.Property(y => y.StartYear).HasColumnName("AcademicYear_StartYear");
                });
            });

            modelBuilder.Entity<Student>(builder =>
            {
                builder.OwnsOne(p => p.AdmissionAcademicYear, year =>
                {
                    year.Ignore(y => y.EndYear);
                    year.Property(y => y.StartYear).HasColumnName("AdmissionAcademicYear_StartYear");
                });

                builder.OwnsOne(p => p.CurrentLevel, level =>
                {
                    level.Property(l => l.Value).HasColumnName("CurrentLevel");
                });

                builder.OwnsOne(p => p.Cgpa, gpa =>
                {
                    gpa.Property(g => g.Value).HasColumnName("Cgpa_Value");
                    gpa.Property(g => g.MaximumValue).HasColumnName("Cgpa_MaximumValue");
                });
            });

            // ==================== DELETE BEHAVIOR ====================

            // SQL Server rejects cascade FKs with multiple cascade paths (error 1785), so cascade
            // is disabled globally; opt individual relationships back in with DeleteBehavior.Cascade.
            var identityEntityNamespace = typeof(IdentityUser).Namespace!;
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                if (foreignKey.IsOwnership)
                {
                    continue;
                }

                if (foreignKey.DeclaringEntityType.ClrType.Namespace == identityEntityNamespace)
                {
                    // Identity's own tables (user claims/logins/tokens/roles) are single-path;
                    // keep cascade so UserManager.DeleteAsync keeps working.
                    continue;
                }

                if (foreignKey.DeleteBehavior == DeleteBehavior.Cascade)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            // Refresh tokens live on a single direct path from AppUser, so cascade is safe.
            modelBuilder.Entity<RefreshToken>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
