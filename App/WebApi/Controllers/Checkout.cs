using App.Domain.Enums;
using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CheckoutControler : ControllerBase
{
    private ICheckoutService _checkoutService;

    public CheckoutControler(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPut("/checkout")]
    [Authorize(Roles = nameof(ERole.CLIENT))]
    public async Task<IResult> Checkout()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _checkoutService.ArchivateCart(int.Parse(identityId));

            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpGet("/checkout/list")]
    [Authorize]
    public async Task<IResult> CheckoutList()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _checkoutService.List(int.Parse(identityId));

            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }
}