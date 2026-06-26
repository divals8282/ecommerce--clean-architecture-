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
        return Results.Json(new { status = true, products }, statusCode: 200);
    }

    [HttpPut("/product/{productId}")]
    [Authorize(Roles = nameof(ERole.CONTENT_MANAGER))]
    public async Task<IResult> Product([FromBody] ProductRequestDTO productDTO)
    {
        var productEntity = await _productService.Add(productDTO);

        return Results.Json(new { status = productEntity != null, product = new ProductResponseDTO
        {
            Id = productEntity != null ? productEntity.Id : 0,
            Name = productEntity != null ? productEntity.Name : string.Empty,
            Price = productEntity != null ? productEntity.Price : 0
        } }, statusCode: 200);
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