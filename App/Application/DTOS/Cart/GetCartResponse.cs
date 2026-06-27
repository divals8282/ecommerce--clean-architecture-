
namespace App.Application.DTOS.Cart;

public class GetCartResponseDTO
{
    public bool Status { get; set; }

    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Price { get; set; }
    }
    public List<Products>? Data { get; set; } = null!;
}