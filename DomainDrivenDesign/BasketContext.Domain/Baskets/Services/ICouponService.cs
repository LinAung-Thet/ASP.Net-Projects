using BasketContext.Domain.Coupons;
using Common.Domain;

namespace BasketContext.Domain.Baskets.Services
{
    public interface ICouponService
    {
        Task<decimal> ApplyDiscountAsync(Id<Coupon> couponId, decimal totalAmount);
        Task<bool> IsActive(Id<Coupon> couponId);
    }
}