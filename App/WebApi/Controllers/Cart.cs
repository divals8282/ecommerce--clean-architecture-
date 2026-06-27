using App.Application.DTOS.Cart;
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

        if(cart == null)
        {
            Response.Cookies.Delete("identity");
        }

        return Results.Json(new GetCartResponseDTO
        {
            Status = cart != null,
            Data = cart?.Products?
                .Select(p => new GetCartResponseDTO.Products
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList()
        }, statusCode: 200);
    }

    [HttpPut("/cart/add/{productId}")]
    public async Task<IResult> AddProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        var possibleNewIdentity = await _cartService.AddProduct(identityId, productId);

        if (identityId == null && possibleNewIdentity != null)
        {
            var identityValue = possibleNewIdentity.ToString();
            if (!string.IsNullOrEmpty(identityValue))
            {
                Response.Cookies.Append("identity", identityValue);
            }
        }

        return Results.Json(new { identity = identityId == null ? possibleNewIdentity.ToString() : identityId }, statusCode: 200);
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