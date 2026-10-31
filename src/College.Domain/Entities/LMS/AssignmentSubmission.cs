using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.LMS
{
    /// <summary>
    /// Composite primary key: (AssignmentId, StudentId).
    /// </summary>
    public class AssignmentSubmission : IEntity
    {
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public string SubmissionText { get; set; } = null!;

        public string AttachmentKey { get; set; } = null!;

        public DateTime SubmittedAt { get; set; }

        public decimal Score { get; set; }

        public string Feedback { get; set; } = null!;
    }
}
