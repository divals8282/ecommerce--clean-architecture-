using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CartController : ControllerBase
{

    private readonly ICartService _cartService;
    private readonly IIdentityService _identityService;

    public CartController(ICartService cartService, IIdentityService identityService)
    {
        _cartService = cartService;
        _identityService = identityService;
    }

    [HttpGet("cart")]
    public async Task<IResult> GetCart()
    {
        var identityId = Request.Cookies["identity"];

        var cart = await _cartService.GetCart(identityId);

        return Results.Json(new { status = cart != null, cart }, statusCode: 200);
    }

    [HttpPut("/cart/add/{productId}")]
    public async Task<IResult> AddProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        var status = await _cartService.AddProduct(identityId, productId);

        return Results.Json(new { status }, statusCode: 200);
    }

    [HttpDelete("/cart/product/{productId}")]
    public async Task<IResult> DeleteProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        var status = await _cartService.DeleteProduct(identityId, productId);

        return Results.Json(new { status }, statusCode: 200);
    }

    [HttpDelete("/cart")]
    public async Task<IResult> Cart()
    {
        var identityId = Request.Cookies["identity"];

        await _cartService.GetCart(identityId);
        await _identityService.DeleteIdentity(identityId);

        Response.Cookies.Delete("identity");

        return Results.Json(new { status = true }, statusCode: 200);
    }
}