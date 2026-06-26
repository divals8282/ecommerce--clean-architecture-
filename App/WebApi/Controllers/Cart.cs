using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CartController : ControllerBase
{

    private ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("cart")]
    public async Task<IResult> GetCart()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var Cart = await _cartService.GetCart(int.Parse(identityId));

            return Results.Json(new { status = true, Cart }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpPut("/cart/add/{productId}")]
    public async Task<IResult> AddProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _cartService.AddProduct(int.Parse(identityId), productId);
            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpDelete("/cart/product/{productId}")]
    public async Task<IResult> DeleteProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _cartService.DeleteProduct(int.Parse(identityId), productId);
            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpDelete("/cart")]
    public async Task<IResult> Cart()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var cart = await _cartService.GetCart(int.Parse(identityId));
            return Results.Json(new { status = cart != null, cart }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }
}