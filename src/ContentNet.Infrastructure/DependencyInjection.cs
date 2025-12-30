using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Abstractions.Services;
using ContentNet.Domain.Entities;
using ContentNet.Infrastructure.Context;
using ContentNet.Infrastructure.Persistence.Repositories;
using ContentNet.Infrastructure.Persistence.UnitOfWork;
using ContentNet.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static ContentNet.Infrastructure.Context.ApplicationDbContext;

namespace ContentNet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("Default")
                   ?? throw new InvalidOperationException("ConnectionStrings: Default is missing.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connection, sql =>
            {
                sql.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Application);
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        // Register Application Repositories
        services.AddScoped<IArticleRepository, ArticleRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ISmsSenderService, SmsSenderService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
        .AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
                )
            };
        });

        return services;
    }
}
