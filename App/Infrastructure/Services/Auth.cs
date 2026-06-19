using App.Domain.Entities;

namespace App.Infrastructure.Services;


public class AuthService {

    // private readonly AppDbContext _db;

    // public AuthRepository(AppDbContext db)
    // {
    //     _db = db;
    // }


    public async Task CheckPasswordValidity(string password) {

    }

    public async Task GenerateTokens(UserEntity user) {

    }

    public async Task CheckRefreshTokenValidity(string refreshToken) {

    }

    public async Task KillActualRefreshToken(UserEntity user) {

    }
}