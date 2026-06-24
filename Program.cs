using System.Text;
using App.Application.Services;
using App.Infrastructure.Presistence;
using App.Infrastructure.Repositories;
using App.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>((options) => {
    options.UseSqlServer();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:SecretKey"]!)),

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();


builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<CardRepository>();
builder.Services.AddScoped<CheckoutRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();