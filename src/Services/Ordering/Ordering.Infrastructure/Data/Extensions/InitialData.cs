namespace Ordering.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("d0eaaaba-d0be-4be1-9a4d-a593c44ea5e1")), "John Doe", "john.doe@example.com"),
        Customer.Create(CustomerId.Of(new Guid("d0eaaaba-d0be-4be1-9a4d-a593c44ea5e2")), "Jane Doe", "jane.doe@example.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("a37d162b-1aa9-4c93-a0ad-c36355e7393f")), "Canon EOS R5", 3899.00M),
        Product.Create(ProductId.Of(new Guid("4bb93446-f17b-4c0a-b2de-b1d6e1e34ff0")), "Fitbit Charge 6", 159.00M),
        Product.Create(ProductId.Of(new Guid("3abb2637-e463-4bb8-b6c9-14c1d5b64278")), "IKEA Poäng Chair", 199.00M),
        Product.Create(ProductId.Of(new Guid("0160ee00-8428-4665-a04a-97ca0961a99d")), "YETI Rambler Tumbler", 35.00M)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("John", "Doe", "john.doe@example.com", "123 Main St.", "South Africa", "Gauteng", "2000");
            var address2 = Address.Of("Jane", "Doe", "jane.doe@example.com", "85 Novel Dr.", "South Africa", "North West", "0299");

            var payment1 = Payment.Of("4111111111111111", "John Doe", "0229", "123", 1);
            var payment2 = Payment.Of("4111111111111112", "Jane Doe", "1130", "456", 1);

            var order1 = Order.Create(
                OrderId.Of(new Guid("b2eaaaba-d0be-4be1-9a4d-a593c44ea5e1")),
                CustomerId.Of(new Guid("d0eaaaba-d0be-4be1-9a4d-a593c44ea5e1")),
                OrderName.Of("00001"),
                shippingAddress: address1,
                billingAddress: address1,
                payment: payment1);
            order1.Add(ProductId.Of(new Guid("a37d162b-1aa9-4c93-a0ad-c36355e7393f")), 1, 3899.00M);
            order1.Add(ProductId.Of(new Guid("4bb93446-f17b-4c0a-b2de-b1d6e1e34ff0")), 2, 159.00M);

            var order2 = Order.Create(
                OrderId.Of(new Guid("c3eaaaba-d0be-4be1-9a4d-a593c44ea5e2")),
                CustomerId.Of(new Guid("d0eaaaba-d0be-4be1-9a4d-a593c44ea5e2")),
                OrderName.Of("00002"),
                shippingAddress: address2,
                billingAddress: address2,
                payment: payment2);
            order2.Add(ProductId.Of(new Guid("4bb93446-f17b-4c0a-b2de-b1d6e1e34ff0")), 1, 159.00M);
            order2.Add(ProductId.Of(new Guid("3abb2637-e463-4bb8-b6c9-14c1d5b64278")), 1, 199.00M);
            order2.Add(ProductId.Of(new Guid("0160ee00-8428-4665-a04a-97ca0961a99d")), 3, 35.00M);

            return new List<Order> { order1, order2 };
        }
    }
}