using College.Domain.Common;
using College.Domain.Enums;

namespace College.Domain.Entities
{
    public class StudentRequest : AggregateRoot
    {
        public RequestType Type { get; set; }

        public RequestStatus Status { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public Guid? PaymentId { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
