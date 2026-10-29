using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities
{
    public class Enrollment : Entity
    {
        public EnrollmentStatus Status { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid SemesterCourseId { get; set; }  
        public SemesterCourse SemesterCourse { get; set; } = null!;
    }
}
