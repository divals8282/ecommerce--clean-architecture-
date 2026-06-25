using App.Application.DTOS.Auth;
using App.Domain.Entities;
using App.Domain.Enums;

namespace App.Application.Services;

public interface IUserService
{
    public Task<bool> SetNewRole(UserEntity user, RoleEnum role);

    public Task<bool> RegisterUser(UserEntity user);

    public Task<SignInResponseDTO> Login(SignInRequestDTO user);

    public bool CheckSuperSecretValidity(string superSecret);

    public Task<UserEntity?> GetCurrentUser();
}