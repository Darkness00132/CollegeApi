using College.Domain.Common;

namespace College.Domain.Entities.LMS
{
    /// <summary>
    /// Composite primary key: (QuizAttemptId, QuestionId).
    /// </summary>
    public class QuizAnswer : IEntity
    {
        public Guid QuizAttemptId { get; set; }
        public QuizAttempt QuizAttempt { get; set; } = null!;

        public Guid QuestionId { get; set; }
        public QuizQuestion Question { get; set; } = null!;

        public Guid? SelectedOptionId { get; set; }
        public QuizOption? SelectedOption { get; set; }

        public string AnswerText { get; set; } = null!;

        public decimal AwardedPoints { get; set; }
    }
}
