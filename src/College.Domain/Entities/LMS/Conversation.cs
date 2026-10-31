using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class Conversation : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string ConversationType { get; set; } = null!;
    }
}
