using College.Domain.Common;

namespace College.Domain.Entities
{
    public class AuditEvent : AggregateRoot
    {
        public DateTime Timestamp { get; set; }

        public string EventType { get; set; } = null!;

        public string ActorType { get; set; } = null!;

        public string? ActorId { get; set; }

        public string TargetId { get; set; } = null!;

        public string Outcome { get; set; } = null!;

        public string AdditionalInfo { get; set; } = null!;
    }
}
