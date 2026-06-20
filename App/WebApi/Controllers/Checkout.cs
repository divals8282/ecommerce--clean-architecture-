using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CheckoutControler : ControllerBase {

    [HttpPut("/checkout/{cardId}")]
    public async Task<IResult> Checkout()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpDelete("/checkout/{productId}")]
    public async Task<IResult> Delete()
    {
        return Results.Json(new { }, statusCode: 200);
    }


    [HttpPost("/product/add")]
    public async Task<IResult> Add(ProductEntity product)
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpPut("/product/edit")]
    public async Task<IResult> Edit(ProductEntity product)
    {
        return Results.Json(new {  }, statusCode: 200);
    }
}