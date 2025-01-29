using MediatR;

namespace Common.Domain.Events
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    { }
}
