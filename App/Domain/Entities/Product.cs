namespace App.Domain.Entities;


public class ProductEntity
{
    public int Id { get; set; }
    required public string Name { get; set; }
    public double Price { get; set; }
}