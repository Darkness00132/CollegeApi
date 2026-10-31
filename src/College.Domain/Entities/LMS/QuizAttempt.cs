using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    public class QuizAttempt : AggregateRoot
    {
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public DateTime StartedAt { get; set; }

        public DateTime SubmittedAt { get; set; }

        public decimal Score { get; set; }
    }
}
