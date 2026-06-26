using App.Application.Enums.JWT;
using App.Domain.Entities;

namespace App.Domain.Interfaces.Authentication.JWT;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserEntity user, ETokenType tokenType);
}