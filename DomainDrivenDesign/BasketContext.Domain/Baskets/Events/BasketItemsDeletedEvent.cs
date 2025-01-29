using Common.Domain;

namespace BasketContext.Domain.Baskets.Events
{
    public sealed class BasketItemsDeletedEvent : BaseBasketDomainEvent
    {
        public BasketItemsDeletedEvent(Id<Basket> basketId)
            : base(basketId) { }

    }
}
