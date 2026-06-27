namespace App.Domain.Entities;


public class CartEntity
{
    public int Id { get; set; }
    public IdentityEntity Identity { get; set; } = null!;
    public List<ProductEntity> Products { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}