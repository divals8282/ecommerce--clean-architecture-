using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers;

[ApiController]
public class AuthController : ControllerBase {
    [HttpPost("/auth/sign-up")]
    public async Task<IResult> SignUp()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpPost("/auth/sign-in")]
    public async Task<IResult> SignIn()
    {
        return Results.Json(new {  }, statusCode: 200);
    }

    [HttpGet("/auth/get-user")]
    public async Task<IResult> GetUser(ProductEntity product)
    {
        return Results.Json(new {  }, statusCode: 200);
    }
}