using App.Application.DTOS.Auth;
using App.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class ProductController : ControllerBase
{

    private ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }


    [HttpGet("/product/list/{limit}/{offset}")]
    public async Task<IResult> List(int limit, int offset)
    {
        var products = await _productService.List(limit, offset);
        return Results.Json(new { status = true, products }, statusCode: 200);
    }

    [HttpGet("/product/{productId}")]
    public async Task<IResult> Product(int productId)
    {
        var product = await _productService.GetProductById(productId);

        return Results.Json(new { status = product != null, product }, statusCode: 200);
    }

    [HttpPost("/product/edit/{productId}")]
    public async Task<IResult> Edit([FromBody] ProductRequestDTO productDTO, int productId)
    {
        var status = await _productService.Edit(productId, productDTO);

        return Results.Json(new { status }, statusCode: 200);
    }


    [HttpDelete("/product/{productId}")]
    public async Task<IResult> Delete(int productId)
    {
        var status = await _productService.Delete(productId);

        return Results.Json(new { status }, statusCode: 200);
    }
}