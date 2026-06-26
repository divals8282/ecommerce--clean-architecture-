namespace App.Domain.Entities;


public class CheckoutEntity
{
    public int Id { get; set; }

    public UserEntity User { get; set; } = null!;

    public List<ProductEntity> Products { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}