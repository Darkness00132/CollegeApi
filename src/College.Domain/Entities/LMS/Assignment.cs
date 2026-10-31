using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class Assignment : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public decimal MaximumScore { get; set; }

        public DateTime DueAt { get; set; }
    }
}
