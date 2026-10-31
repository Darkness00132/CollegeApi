using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities.Affairs
{
    public class StudentComplaint : AggregateRoot
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ComplaintStatus Status { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public string? ResolutionDetails { get; set; }
    }
}
