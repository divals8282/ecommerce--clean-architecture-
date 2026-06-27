namespace App.Domain.Entities;


public class IdentityEntity
{
    public int Id { get; set; }

    public int CartId { get; set; }
    public CartEntity Cart { get; set; } = null!;
}