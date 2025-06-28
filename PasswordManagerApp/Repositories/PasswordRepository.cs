using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Data;
using PasswordManagerApp.Models;
using PasswordManagerApp.Repositories;

namespace PasswordManagerApp.Repositories;

public class PasswordRepository : IPasswordRepository
{
    private readonly AppDbContext _context;

    public PasswordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PasswordEntry>> GetAllAsync(int userId)
    {
        return await _context.Passwords
            .Where(p => p.UserId == userId)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<PasswordEntry?> GetByIdAsync(int id, int userId)
    {
        return await _context.Passwords
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
    }

    public async Task AddAsync(PasswordEntry password, int userId)
    {
        password.UserId = userId;
        await _context.Passwords.AddAsync(password);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PasswordEntry password, int userId)
    {
        var existing = await _context.Passwords
            .FirstOrDefaultAsync(p => p.Id == password.Id && p.UserId == userId);

        if (existing != null)
        {
            existing.Title = password.Title;
            existing.Url = password.Url;
            existing.Username = password.Username;
            existing.Password = password.Password;
            existing.CategoryId = password.CategoryId;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var password = await _context.Passwords
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        if (password != null)
        {
            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}