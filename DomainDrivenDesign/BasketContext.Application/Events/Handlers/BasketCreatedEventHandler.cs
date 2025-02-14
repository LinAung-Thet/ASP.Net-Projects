using BasketContext.Application.Abstractions.Messaging;
using BasketContext.Application.Events.Integration;
using BasketContext.Domain.Baskets.Events;
using Common.Domain.Events;

namespace BasketContext.Application.Events.Handlers
{
    public class BasketCreatedDomainEventHandler : IDomainEventHandler<BasketCreatedEvent>
    {
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        public BasketCreatedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
        {
            _integrationEventPublisher = integrationEventPublisher;
        }

        public async Task Handle(BasketCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new BasketCreatedIntegrationEvent(domainEvent.AggregateId, domainEvent.CustomerId);
            await _integrationEventPublisher.PublishAsync(integrationEvent);
        }
    }
}
