namespace App.Domain.Entities;


public class CheckoutEntity
{
    public int Id { get; set; }

    public UserEntity User = null!;

    public List<ProductEntity> Products = null!;

    public DateTime CreatedAt { get; set; }
}