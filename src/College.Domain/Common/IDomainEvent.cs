using MediatR;

namespace College.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        DateTimeOffset OccurredAt { get; }
        DateTimeOffset ProcessedAt { get; }
    }
}
