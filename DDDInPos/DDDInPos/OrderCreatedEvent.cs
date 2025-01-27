public class OrderCreatedEvent
{
    public Guid OrderId { get; }
    public int CustomerId { get; }

    public OrderCreatedEvent(Guid orderId, int customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
    }
}