using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Presistence;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<CartEntity> Carts => Set<CartEntity>();
    public DbSet<IdentityEntity> Identites => Set<IdentityEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<CheckoutEntity> Checkouts => Set<CheckoutEntity>();

}