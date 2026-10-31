using College.Domain.Common;

namespace College.Domain.Entities.LMS
{
    public class QuizOption : Entity
    {
        public Guid QuestionId { get; set; }
        public QuizQuestion Question { get; set; } = null!;

        public string OptionText { get; set; } = null!;

        public bool IsCorrect { get; set; }
    }
}
