using App.Application.DTOS.Auth;
using App.Domain.Entities;
using App.Domain.Enums;

namespace App.Domain.Interfaces.Services;

public interface IUserService
{
    public Task<bool> SetNewRole(UserEntity user, ERole role);

    public Task<bool> RegisterUser(UserEntity user);

    public Task<string[]?> Login(SignInRequestDTO user);

    public bool CheckSuperSecretValidity(string superSecret);

    public Task<UserEntity?> GetCurrentUser();
}