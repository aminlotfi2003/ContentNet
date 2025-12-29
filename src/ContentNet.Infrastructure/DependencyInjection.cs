using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Infrastructure.Context;
using ContentNet.Infrastructure.Persistence.Repositories;
using ContentNet.Infrastructure.Persistence.UnitOfWork;
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

        return services;
    }
}
