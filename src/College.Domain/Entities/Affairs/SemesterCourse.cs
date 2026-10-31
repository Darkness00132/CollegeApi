using College.Domain.Common;
using College.Domain.Enums;
using College.Domain.ValueObjects;

namespace College.Domain.Entities.Affairs
{
    public class SemesterCourse : AggregateRoot
    {
        public SemesterCourseStatus Status { get; set; }

        public AcademicLevel Level { get; set; } = null!;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;
    }
}
