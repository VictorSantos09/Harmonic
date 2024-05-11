using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Harmonic.API.Context;
public class ApplicationDbContext : IdentityDbContext<HarmonicIdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<HarmonicIdentityUser>(b =>
        {
            b.ToTable("USERS");
            b.Property(x => x.UserName).IsRequired();
            b.Property(x => x.NormalizedUserName).IsRequired();
            b.Property(x => x.Email).IsRequired();
            b.Property(x => x.NormalizedEmail).IsRequired();
        });

        builder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable("USERS_CLAIMS");

        });

        builder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable("USERS_LOGINS");

        });

        builder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable("USERS_TOKENS");

        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable("ROLES");
        });

        builder.Entity<IdentityRoleClaim<string>>(b =>
        {
            b.ToTable("ROLES_CLAIMS");
        });

        builder.Entity<IdentityUserRole<string>>(b =>
        {

            b.ToTable("USERS_ROLES");

        });
    }
}
