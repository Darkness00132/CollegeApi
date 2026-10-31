using College.Domain.Common;
using College.Domain.Entities.Affairs;
using College.Domain.Entities.Identity;

namespace College.Domain.Entities.LMS
{
    public class CourseAnnouncement : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public Guid CreatedBy { get; set; }
        public AppUser CreatedByUser { get; set; } = null!;
    }
}
