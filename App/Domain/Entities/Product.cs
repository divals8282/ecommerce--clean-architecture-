namespace App.Domain.Entities;


public class ProductEntity
{
    public int Id { get; set; }
    required public string Name { get; set; }
    required public int Price { get; set; }
    public List<CartEntity> Carts { get; set; } = null!;
    public List<CheckoutEntity> Checkouts { get; set; } = null!;
    
}