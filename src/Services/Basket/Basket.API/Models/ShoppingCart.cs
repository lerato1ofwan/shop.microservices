namespace Basket.API.Models;

// Aggregate Root (Shopping Cart and ShoppingCartItem is the Entity/Json document table using Marten)
// ShoppingCart contains a list of ShoppingCartItems and stored as a single document (json format) in the database
public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice =>
        Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string UserName)
    {
        this.UserName = UserName;
    }

    // Mapping constructor
    public ShoppingCart()
    {

    }
}