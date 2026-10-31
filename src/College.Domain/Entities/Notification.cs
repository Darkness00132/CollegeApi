using College.Domain.Common;
using College.Domain.Entities.Identity;

namespace College.Domain.Entities
{
    public class Notification : AggregateRoot
    {
        public Guid RecipientUserId { get; set; }
        public AppUser Recipient { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Channel { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? ReadAt { get; set; }
    }
}
