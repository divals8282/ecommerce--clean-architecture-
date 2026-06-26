using System.ComponentModel.DataAnnotations;

namespace App.Application.DTOS.Product;

public class ProductResponseDTO
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    [Required]
    public required double Price { get; set; }
}