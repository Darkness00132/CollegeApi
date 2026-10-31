using College.Domain.Common;
using College.Domain.Entities.Identity;

namespace College.Domain.Entities.LMS
{
    /// <summary>
    /// Composite primary key: (ConversationId, UserId).
    /// </summary>
    public class ConversationParticipant : IEntity
    {
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;

        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public DateTime JoinedAt { get; set; }
    }
}
