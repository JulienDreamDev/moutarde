using Microsoft.EntityFrameworkCore;
using moutarde_back.Entities;

namespace moutarde_back.Infrastructure.Data;

public class MoutardeDbContext(DbContextOptions<MoutardeDbContext> options, ILogger<MoutardeDbContext> logger) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        logger.LogInformation("Creating User database schema...");
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Username).IsRequired().HasMaxLength(20);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.CreatedAt).IsRequired();
        });
    }
}