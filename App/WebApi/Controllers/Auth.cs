using App.Application.DTOS.Auth;
using App.Application.Services;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Enums;
namespace App.WebApi.Controllers;

[ApiController]
public class AuthController : ControllerBase {

    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/auth/sign-up")]    
    public async Task<IResult> SignUp([FromBody] SignUpRequestDTO request)
    {
        var newUser = new UserEntity(){
            UserName = request.UserName,
            LastName = request.LastName,
            Name = request.Name,
            Password = request.Password,
            Role = RoleEnum.CLIENT,
            Checkouts = new List<CheckoutEntity>(),
            RefreshToken = ""
        };

        var registrationStatus = await _userService.RegisterUser(newUser);

        return Results.Json(new { status = registrationStatus }, statusCode: 200);
    }

    [HttpPost("/auth/sign-in")]
    public async Task<IResult> SignIn([FromBody] SignInRequestDTO request)
    {
        var result = await _userService.Login(request);

        return Results.Json(result, statusCode: 200);
    }

    [HttpPost("/auth/sign-up/content-manager/{superSecret}")]
    public async Task<IResult> SignUpContentManager([FromBody] SignUpRequestDTO request, [FromRoute] string superSecret) {
        var isSuperSecretValid = _userService.CheckSuperSecretValidity(superSecret);
        
        if(!isSuperSecretValid) {
            return Results.Json(new { message = "SUPER_SECRET not valid"}, statusCode: 200);
        }
        
        var registrationStatus = await _userService.RegisterUser(new UserEntity() {
            UserName = request.UserName,
            LastName = request.LastName,
            Name = request.Name,
            Password = request.Password,
            Role = RoleEnum.CLIENT,
            Checkouts = new List<CheckoutEntity>(),
            RefreshToken = ""
        });


        return Results.Json(new { status = registrationStatus }, statusCode: 200);
    }

    [HttpGet("/auth/get-user")]
    public async Task<IResult> GetUser()
    {
        return Results.Json(new {  }, statusCode: 200);
    }
}