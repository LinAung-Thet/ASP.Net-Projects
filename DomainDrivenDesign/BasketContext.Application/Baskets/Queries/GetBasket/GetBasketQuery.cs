using BasketContext.Application.Abstractions;
using BasketContext.Application.Baskets.Dtos;

namespace BasketContext.Application.Baskets.Queries.GetBasket
{
    public record GetBasketQuery(Guid BasketId) : IQuery<BasketDto>;

}
