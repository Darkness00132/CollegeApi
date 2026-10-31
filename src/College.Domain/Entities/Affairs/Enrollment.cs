using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities.Affairs
{
    /// <summary>
    /// Composite primary key: (SemesterRegistrationId, SemesterCourseId).
    /// </summary>
    public class Enrollment : IEntity
    {
        public EnrollmentStatus Status { get; set; }

        public Guid SemesterRegistrationId { get; set; }
        public SemesterRegistration SemesterRegistration { get; set; } = null!;

        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid? ReviewedByProfessorId { get; set; }
        public ProfessorProfile? ReviewedByProfessor { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string? RejectionReason { get; set; }

        public DateTime? EnrolledAt { get; set; }

        public DateTime? WithdrawnAt { get; set; }
    }
}
