using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Email).IsRequired().HasMaxLength(320);
            e.HasIndex(x => x.Email).IsUnique();

            e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(300);
            e.Property(x => x.Role).IsRequired().HasMaxLength(50);

            e.HasMany(x => x.RefreshTokens)
             .WithOne(x => x.User)
             .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<RefreshToken>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Token).IsRequired().HasMaxLength(500);
            e.HasIndex(x => x.Token).IsUnique();

            e.Property(x => x.ExpiresAtUtc).IsRequired();
        });
    }
}