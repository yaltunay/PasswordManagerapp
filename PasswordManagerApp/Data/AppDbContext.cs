using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Models;

namespace PasswordManagerApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<PasswordEntry> Passwords { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Sosyal Medya" },
            new Category { Id = 2, Name = "E-Posta" },
            new Category { Id = 3, Name = "Bankacılık" },
            new Category { Id = 4, Name = "Oyun" },
            new Category { Id = 5, Name = "Diğer" }
        );
    }
}