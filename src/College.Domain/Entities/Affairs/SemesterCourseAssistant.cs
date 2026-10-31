using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    /// <summary>
    /// Composite primary key: (SemesterCourseId, AcademicAssistantProfileId).
    /// </summary>
    public class SemesterCourseAssistant : IEntity
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid AcademicAssistantProfileId { get; set; }
        public AcademicAssistantProfile AcademicAssistantProfile { get; set; } = null!;
    }
}
