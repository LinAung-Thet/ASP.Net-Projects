using MediatR;

namespace BasketContext.Application.Baskets.Commands.CalculateTotalAmount
{
    public record CalculateTotalAmountCommand(Guid BasketId) : IRequest;

}
