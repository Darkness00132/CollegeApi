using College.Domain.Common;

namespace College.Domain.Entities.LMS
{
    public class QuizQuestion : Entity
    {
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public string QuestionText { get; set; } = null!;

        public string QuestionType { get; set; } = null!;

        public decimal Points { get; set; }

        public int SortOrder { get; set; }
    }
}
