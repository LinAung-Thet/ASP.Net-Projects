using BasketContext.Domain.Common;
using Common.Domain.Events;
using Common.Domain.Events.Decorators;

namespace BasketContext.Domain.Baskets.Events
{
    [AggregateType(BasketEventConstants.BasketsAggregateTypeName)]
    public abstract class BaseBasketDomainEvent(Guid aggregateId, DateTimeOffset? occurredOnUtc = null)
        : DomainEvent(aggregateId, occurredOnUtc ?? DateTimeOffset.UtcNow) { }
}
