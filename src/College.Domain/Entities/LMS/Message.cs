using College.Domain.Common;
using College.Domain.Entities.Identity;

namespace College.Domain.Entities.LMS
{
    public class Message : Entity
    {
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;

        public Guid SenderId { get; set; }
        public AppUser Sender { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
