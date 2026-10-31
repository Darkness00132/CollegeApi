using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class Quiz : AggregateRoot
    {
        public Guid SemesterCourseId { get; set; }
        public SemesterCourse SemesterCourse { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int DurationMinutes { get; set; }

        public decimal MaximumScore { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }

        public int MaximumAttempts { get; set; }
    }
}
