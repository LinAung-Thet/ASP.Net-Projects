using BasketContext.Application.Abstractions;

namespace BasketContext.Application.Coupons
{
    public record ApplyCouponCommand(Guid BasketId, Guid CouponId) : IQuery;

}
