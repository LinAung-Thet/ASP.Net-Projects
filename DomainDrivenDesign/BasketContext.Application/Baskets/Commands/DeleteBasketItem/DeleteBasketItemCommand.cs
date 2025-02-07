using BasketContext.Application.Abstractions;

namespace BasketContext.Application.Baskets.Commands.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid BasketId, Guid ItemId) : ICommand;

}
