using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class CourseMaterial : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string MaterialType { get; set; } = null!;

        public string AttachmentKey { get; set; } = null!;
    }
}
