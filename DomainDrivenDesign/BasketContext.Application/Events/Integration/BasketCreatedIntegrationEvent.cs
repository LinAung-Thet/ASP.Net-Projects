using BasketContext.Domain.Baskets;
using Common.Core.Events;
using Common.Domain;

namespace BasketContext.Application.Events.Integration
{
    public sealed class BasketCreatedIntegrationEvent : IntegrationEvent
    {
        public BasketCreatedIntegrationEvent(Id<Basket> basketId, Guid customerId)
            : base(basketId)
        {
            CustomerId = customerId;
        }
        public BasketCreatedIntegrationEvent() { }

        public Guid CustomerId { get; set; }
    }
}
