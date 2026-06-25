using App.Application.DTOS.Auth;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;

namespace App.Application.Services;

public class ProductService
{
    private IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<List<ProductEntity>> List(int limit, int offset)
    {


        return await _productRepo.GetList(limit, offset);
    }

    public async Task<ProductEntity?> GetProductById(int productId)
    {
        return await _productRepo.GetByIdAsync(productId);
    }

    public async Task<ProductEntity?> Add(ProductRequestDTO product)
    {
        var newProduct = new ProductEntity()
        {
            Name = product.Name,
            price = product.price
        };

        await _productRepo.Add(newProduct);

        return newProduct;
    }

    public async Task<bool> Delete(int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);

        if (product != null)
        {
            await _productRepo.Delete(product);

            return true;
        }

        return false;
    }

    public async Task<bool> Edit(int productId, ProductRequestDTO productDTO)
    {
        var product = await _productRepo.GetByIdAsync(productId);

        if (product == null)
        {
            return false;
        }

        product.Name = productDTO.Name;
        product.price = productDTO.price;

        await _productRepo.SaveChangesAsync();

        return true;
    }
}