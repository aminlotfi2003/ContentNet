using ContentNet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContentNet.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public static class Schemas
    {
        public const string Application = "app";
        public const string Identity = "identity";
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Register Fluent API Configurations
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Entity<OtpCode>().ToTable("OtpCodes", Schemas.Identity);
        builder.Entity<User>().ToTable("Users", Schemas.Identity);
        builder.Entity<IdentityRole<int>>().ToTable("Roles", Schemas.Identity);
        builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", Schemas.Identity);
        builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", Schemas.Identity);
        builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", Schemas.Identity);
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", Schemas.Identity);
        builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", Schemas.Identity);
    }
}
