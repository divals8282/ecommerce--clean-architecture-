using System.ComponentModel.DataAnnotations;

namespace App.Application.DTOS.Product;

public class ProductRequestDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(24)]
    public required string Name { get; set; }

    [Required]
    public required int Price { get; set; }
}