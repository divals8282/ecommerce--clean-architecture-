using App.Application.Services;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class CardController : ControllerBase
{

    private CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("card")]
    public async Task<IResult> GetCard()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var Card = await _cardService.GetCard(int.Parse(identityId));

            return Results.Json(new { status = true, Card }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpPut("/card/add/{productId}")]
    public async Task<IResult> AddProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _cardService.AddProduct(int.Parse(identityId), productId);
            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpDelete("/card/product/{productId}")]
    public async Task<IResult> DeleteProduct(int productId)
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var status = await _cardService.DeleteProduct(int.Parse(identityId), productId);
            return Results.Json(new { status }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }

    [HttpDelete("/card")]
    public async Task<IResult> Card()
    {
        var identityId = Request.Cookies["identity"];

        if (identityId != null)
        {
            var card = await _cardService.GetCard(int.Parse(identityId));
            return Results.Json(new { status = card != null, card }, statusCode: 200);
        }

        return Results.Json(new { status = false }, statusCode: 200);
    }
}