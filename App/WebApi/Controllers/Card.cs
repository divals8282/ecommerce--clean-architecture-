using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CardController : ControllerBase {

    [HttpGet("card")]
    public async Task<IResult> GetCard() {

        return Results.Json(new {}, statusCode: 200);
    }

    [HttpPut("/card/add/{productId}")]
    public async Task<IResult> AddProduct() 
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpDelete("/card/product/{productId}")]
    public async Task<IResult> DeleteProduct()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpDelete("/card")]
    public async Task<IResult> Card(ProductEntity product)
    {
        return Results.Json(new {  }, statusCode: 200);
    }
}