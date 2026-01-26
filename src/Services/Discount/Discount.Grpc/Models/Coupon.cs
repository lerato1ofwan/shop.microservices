namespace Discount.Grpc.Models;

public class Coupon
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; }
}