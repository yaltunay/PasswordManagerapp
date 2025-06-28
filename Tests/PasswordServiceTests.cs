using Xunit;
using PasswordManagerApp.Data;
using PasswordManagerApp.Models;
using PasswordManagerApp.Repositories;
using PasswordManagerApp.Services;
using Microsoft.EntityFrameworkCore;

namespace PasswordManagerApp.Tests;

public class PasswordServiceTests : IDisposable
{
    private readonly AppDbContext _context;

    public PasswordServiceTests()
    {
        // InMemory veritabanı ayarları
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);

        // Seed data ekleme
        _context.Categories.Add(new Category { Id = 1, Name = "Sosyal Medya" });
        _context.Users.Add(new User { Id = 1, Username = "testuser", PasswordHash = "hash" });
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddPasswordAsync_Should_Add_Password_For_Valid_User()
    {
        // Arrange
        var repository = new PasswordRepository(_context);
        var service = new PasswordService(repository);
        var password = new PasswordEntry
        {
            Title = "Facebook",
            Url = "https://facebook.com ",
            Username = "testuser",
            Password = "pass123",
            CategoryId = 1
        };

        var userId = 1;

        // Act
        await service.AddPasswordAsync(password, userId);
        var result = await service.GetUserPasswordsAsync(userId);

        // Assert
        Assert.Single(result); // Tek bir şifre olmalı
        Assert.Contains(result, p => p.Title == "Facebook");
    }

    [Fact]
    public async Task GetPasswordByIdAsync_Should_Return_Null_If_Not_Found()
    {
        // Arrange
        var repository = new PasswordRepository(_context);
        var service = new PasswordService(repository);

        var userId = 1;
        var invalidId = 999;

        // Act
        var result = await service.GetPasswordByIdAsync(invalidId, userId);

        // Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}