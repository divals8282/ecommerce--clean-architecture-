using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Application.Enums.JWT;
using App.Domain.Entities;
using App.Domain.Interfaces.Authentication.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Infrastructure.Authentication.JWT;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(UserEntity user, ETokenType tokenType)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.AuthenticationMethod, tokenType == ETokenType.ACCESS ? "ACCESS" : "REFRESH")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SECRET_KEY));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);



        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(tokenType == ETokenType.ACCESS ? 15 : (24 * 60)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}