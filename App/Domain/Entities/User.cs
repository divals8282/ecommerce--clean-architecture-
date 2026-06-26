using App.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Entities;


[Index(nameof(UserName), IsUnique = true)]
public class UserEntity
{
    public int Id { get; set; }

    required public string UserName { get; set; }
    required public string Name { get; set; }
    required public string LastName { get; set; }
    required public string RefreshToken { get; set; }

    required public string Password { get; set; }

    public List<CheckoutEntity> Checkouts { get; set; } = null!;

    required public ERole Role { get; set; }
}