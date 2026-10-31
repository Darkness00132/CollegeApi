using College.Domain.Common;

namespace College.Domain.Entities.Payment
{
    public class PaymentAttempt : Entity
    {
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        public string? ProviderTransactionId { get; set; }

        public string Status { get; set; } = null!;

        public string? FailureReason { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
