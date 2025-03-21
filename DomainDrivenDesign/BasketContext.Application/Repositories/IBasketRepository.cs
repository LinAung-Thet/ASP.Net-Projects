﻿using BasketContext.Domain.Baskets;

namespace BasketContext.Application.Repositories
{
    public interface IBasketRepository : IRepository<Basket> {

        Task AddBasketItemAsync(Guid basketId, BasketItem basketItem);
        Task<bool> IsExistByCustomerIdAsync(Guid id);
    }

}
