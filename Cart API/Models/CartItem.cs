namespace Cart_API.Models;

public class CartItem
{
    public int Id { get; set; }
    public string product { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
}