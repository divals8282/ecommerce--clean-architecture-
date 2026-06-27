using App.Domain.Enums;

namespace App.Application.DTOS.Auth;

public class GetUserResponseDTO
{
    public required bool Status { get; set; }
    public class GetUserResponseUserDTO
    {
        public required string UserName { get; set; } = null!;
        public required string Name { get; set; } = null!;
        public required string LastName { get; set; } = null!;

        public required ERole Role { get; set; } = ERole.CLIENT;
    }
    public GetUserResponseUserDTO? Data;
}