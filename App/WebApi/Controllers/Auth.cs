using App.Application.DTOS.Auth;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.Domain.Interfaces.Services;
using App.Domain.Enums;

namespace App.WebApi.Controllers;

[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/auth/sign-up")]
    public async Task<IResult> SignUp([FromBody] SignUpRequestDTO request)
    {
        var newUser = new UserEntity()
        {
            UserName = request.UserName,
            LastName = request.LastName,
            Name = request.Name,
            Password = request.Password,
            Role = ERole.CLIENT,
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
        if (result == null)
        {
            return Results.Json(new { status = false, message = "Username or Password invalid" }, statusCode: 200);
        }

        return Results.Json(new SignInResponseDTO
        {
            status = true,
            message = "Success",
            data = new SignInResponseDTO.TokensDTO {
                accessToken = result[0],
                refreshToken = result[1]
            }
        }, statusCode: 200);
    }

    [HttpPost("/auth/sign-up/content-manager/{superSecret}")]
    public async Task<IResult> SignUpContentManager([FromBody] SignUpRequestDTO request, [FromRoute] string superSecret)
    {
        var isSuperSecretValid = _userService.CheckSuperSecretValidity(superSecret);

        if (!isSuperSecretValid)
        {
            return Results.Json(new { message = "SUPER_SECRET not valid" }, statusCode: 200);
        }

        var registrationStatus = await _userService.RegisterUser(new UserEntity()
        {
            UserName = request.UserName,
            LastName = request.LastName,
            Name = request.Name,
            Password = request.Password,
            Role = ERole.CLIENT,
            Checkouts = new List<CheckoutEntity>(),
            RefreshToken = ""
        });


        return Results.Json(new { status = registrationStatus }, statusCode: 200);
    }

    [Authorize]
    [HttpGet("/auth/get-user")]
    public async Task<IResult> GetUser()
    {
        var u = await _userService.GetCurrentUser();
        
        var response = new GetUserResponseDTO
        {
            status = u != null ? true : false,
            data = u != null ? new GetUserResponseDTO.GetUserResponseUserDTO
            {
                LastName = u.LastName,
                Name = u.Name,
                Role = u.Role,
                UserName = u.UserName
            } : null    
        };

        return Results.Json(response, statusCode: 200);
    }
}