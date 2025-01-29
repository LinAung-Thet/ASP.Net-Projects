using Common.Domain;

namespace BasketContext.Domain.Coupons.Events
{
    public sealed class CouponDeactivatedEvent :BaseCouponDomainEvent
    {
        public CouponDeactivatedEvent(Id<Coupon> id) : base(id) { }
    }
}