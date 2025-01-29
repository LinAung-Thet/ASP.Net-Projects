using BasketContext.Domain.Common;
using Common.Domain;
using Common.Domain.Events;
using Common.Domain.Events.Decorators;

namespace BasketContext.Domain.Coupons.Events
{
    [AggregateType(BasketEventConstants.CouponsAggregateTypeName)]
    public abstract class BaseCouponDomainEvent(Id<Coupon> aggregateId, DateTimeOffset? occurredOnUtc = null)
        : DomainEvent(aggregateId, occurredOnUtc ?? DateTimeOffset.UtcNow) { }
}
