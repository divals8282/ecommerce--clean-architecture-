using System.ComponentModel.DataAnnotations;

namespace App.Application.DTOS.Auth;

public class SignInResponseDTO
{
    public bool status {get; set;}
    public string message {get; set;} = null!;

    public class TokensDTO {
        public string accessToken {get; set;} = null!;
        public string refreshToken {get; set;} = null!;
    }
    public TokensDTO data {get; set;} = null!;
}