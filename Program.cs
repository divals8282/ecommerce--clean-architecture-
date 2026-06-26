using System.Text;
using App.Application.Services;
using App.Domain.Interfaces.Authentication.JWT;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using App.Infrastructure.Authentication.JWT;
using App.Infrastructure.Presistence;
using App.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<AppOptions>(builder.Configuration.GetSection("APP"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["JWT:SECRET_KEY"]!)),
            ValidateLifetime = true,
        };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();