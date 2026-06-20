using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class ProductController : ControllerBase {
    [HttpGet("/product/list")]
    public async Task<IResult> List()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpGet("/product/{productId}")]
    public async Task<IResult> Product()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpPost("/product/edit")]
    public async Task<IResult> Edit(ProductEntity product)
    {
        return Results.Json(new {  }, statusCode: 200);
    }


    [HttpDelete("/product/{productId}")]
    public async Task<IResult> Delete()
    {
        return Results.Json(new { }, statusCode: 200);
    }
}