namespace App.Domain.Entities;


public class CartEntity
{
    public int Id { get; set; }
    public IdentityEntity Identity = null!;
    public List<ProductEntity> Products = null!;
    public DateTime CreatedAt;
}