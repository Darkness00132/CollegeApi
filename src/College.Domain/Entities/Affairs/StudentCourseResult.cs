using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    /// <summary>
    /// Composite primary key: (SemesterRegistrationId, SemesterCourseId),
    /// referencing the matching Enrollment.
    /// </summary>
    public class StudentCourseResult : IEntity
    {
        public Guid SemesterRegistrationId { get; set; }
        public SemesterRegistration SemesterRegistration { get; set; } = null!;

        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public decimal FinalScore { get; set; }

        public string? LetterGrade { get; set; }

        public decimal? GradePoints { get; set; }

        public bool IsPassed { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}
