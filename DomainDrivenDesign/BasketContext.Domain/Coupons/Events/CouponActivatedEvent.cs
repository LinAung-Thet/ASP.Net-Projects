﻿using Common.Domain;

namespace BasketContext.Domain.Coupons.Events
{
    public sealed class CouponActivatedEvent : BaseCouponDomainEvent
    {
        public CouponActivatedEvent(Id<Coupon> id):base(id) { }
    }
}