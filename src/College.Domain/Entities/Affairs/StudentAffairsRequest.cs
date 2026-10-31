using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities.Affairs
{
    public class StudentAffairsRequest : AggregateRoot
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public RequestType RequestType { get; set; }

        public RequestStatus Status { get; set; }

        public string? Description { get; set; }

        public string? AttachmentKey { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
