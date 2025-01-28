using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DDDInPos.Order;

Console.WriteLine("Hello world!");

namespace DDDInPos
{

    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public static Money Zero => new Money(0, "USD");
        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");
            Amount = amount;
            Currency = currency;
        }
        public static Money operator +(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
                throw new InvalidOperationException("Cannot add money of different currencies");
            return new Money(money1.Amount + money2.Amount, money1.Currency);
        }
        public static Money operator *(Money money, int multiplier)
        {
            return new Money(money.Amount * multiplier, money.Currency);
        }
    }

    public class Customer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public interface IOrderRepository
    {
        Customer GetCustomerById(int customerId);
        Product GetProductById(int productId);
        void SaveOrder(Order order);
    }

    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public int Stock { get; private set; }
        public Product(int id, string name, Money price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }

    public class Order
    {
        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public Money TotalPrice { get; private set; }

        private Order(Customer customer)
        {
            Id = Guid.NewGuid();
            Customer = customer;
            Items = new List<OrderItem>();
            TotalPrice = Money.Zero;
        }

        public List<object> DomainEvents { get; private set; } = new List<object>();

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
        public static Order Create(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Customer cannot be null");

            var order = new Order(customer);
            order.DomainEvents.Add(new OrderCreatedEvent(order.Id, customer.Id));
            return order;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product.Stock < quantity)
                throw new InvalidOperationException("Insufficient stock for product");

            var orderItem = new OrderItem(product, quantity);
            Items.Add(orderItem);

            TotalPrice += orderItem.TotalPrice;
        }
    }

    public class OrderItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public Money TotalPrice => Product.Price * Quantity;

        public OrderItem(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            Product = product;
            Quantity = quantity;
        }
    }

    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void CreateOrder(int customerId, List<(int productId, int quantity)> productDetails)
        {
            var customer = _orderRepository.GetCustomerById(customerId);
            var order = Order.Create(customer);

            foreach (var (productId, quantity) in productDetails)
            {
                var product = _orderRepository.GetProductById(productId);
                order.AddItem(product, quantity);
            }

            _orderRepository.SaveOrder(order);
        }
    }
}