namespace App.Domain.Entities;


public class IdentityEntity
{
    public int Id { get; set; }
    public CardEntity Card = null!;
}