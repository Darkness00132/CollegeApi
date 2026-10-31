using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    /// <summary>
    /// Composite primary key: (SemesterCourseId, ProfessorId).
    /// </summary>
    public class SemesterCourseProfessor : IEntity
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public Guid ProfessorId { get; set; }
        public ProfessorProfile Professor { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
    }
}
