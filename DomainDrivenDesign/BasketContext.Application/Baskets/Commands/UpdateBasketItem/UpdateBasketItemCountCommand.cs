using BasketContext.Application.Abstractions;

namespace BasketContext.Application.Baskets.Commands.UpdateBasketItem
{
    public record UpdateBasketItemCountCommand(Guid BasketId, Guid ItemId, int Quantity) : ICommand;

}
