using App.Domain.Enums;

namespace App.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }
    required public string UserName { get; set; }
    required public string Name { get; set; }
    required public string LastName { get; set; }
    required public string RefreshToken { get; set; }

    required public string Password { get; set; }

    public List<CheckoutEntity> Checkouts = null!;

    required public RoleEnum Role { get; set; }
}