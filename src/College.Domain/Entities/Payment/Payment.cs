using College.Domain.Common;
using College.Domain.Entities.Affairs;

namespace College.Domain.Entities.Payment
{
    public class Payment : AggregateRoot
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = null!;

        public string PaymentPurpose { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? PaidAt { get; set; }
    }
}
