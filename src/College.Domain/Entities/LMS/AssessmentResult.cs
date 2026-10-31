using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    /// <summary>
    /// Composite primary key: (AssessmentId, StudentId).
    /// </summary>
    public class AssessmentResult : IEntity
    {
        public Guid AssessmentId { get; set; }
        public Assessment Assessment { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public decimal Score { get; set; }

        public string LetterGrade { get; set; } = null!;

        public string Feedback { get; set; } = null!;
    }
}
