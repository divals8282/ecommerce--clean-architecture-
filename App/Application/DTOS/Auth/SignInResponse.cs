namespace App.Application.DTOS.Auth;

public class SignInResponseDTO
{
    public bool Status { get; set; }
    public string Message { get; set; } = null!;

    public class TokensDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
    public TokensDTO Data { get; set; } = null!;
}