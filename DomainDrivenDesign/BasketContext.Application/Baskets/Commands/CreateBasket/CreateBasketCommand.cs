using BasketContext.Application.Abstractions;
using BasketContext.Application.Baskets.Dtos;

namespace BasketContext.Application.Baskets.Commands.CreateBasket
{
    public record CreateBasketCommand(decimal TaxPercentage, CustomerDto Customer) : ICommand<Guid>;

}
