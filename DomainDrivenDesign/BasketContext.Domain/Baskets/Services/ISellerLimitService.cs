﻿namespace BasketContext.Domain.Baskets.Services
{
    public interface ISellerLimitService
    {
        int GetLimitForProduct(Guid sellerId, string productName);
    }

}
