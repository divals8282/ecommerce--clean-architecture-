using App.Application.DTOS.Product;
using App.Domain.Enums;
using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet("/product/list/{limit}/{offset}")]
    public async Task<IResult> List(int limit, int offset)
    {
        var products = await _productService.List(limit, offset);
        var total = await _productService.Count();
        return Results.Json(new { status = true, products, total }, statusCode: 200);
    }

    [HttpPut("/product")]
    [Authorize(Roles = nameof(ERole.CONTENT_MANAGER))]
    public async Task<IResult> Product([FromBody] ProductRequestDTO productDTO)
    {
        var product = await _productService.Add(productDTO);

        return Results.Json(new
        {
            status = product != null,
            product = product != null ? new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            } : null
        }, statusCode: 200);
    }

    [HttpPost("/product/edit/{productId}")]
    [Authorize(Roles = nameof(ERole.CONTENT_MANAGER))]
    public async Task<IResult> Edit([FromBody] ProductRequestDTO productDTO, int productId)
    {
        var status = await _productService.Edit(productId, productDTO);

        return Results.Json(new { status }, statusCode: 200);
    }

    [HttpDelete("/product/{productId}")]
    [Authorize(Roles = nameof(ERole.CONTENT_MANAGER))]
    public async Task<IResult> Delete(int productId)
    {
        var status = await _productService.Delete(productId);

        return Results.Json(new { status }, statusCode: 200);
    }
}