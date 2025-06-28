using PasswordManagerApp.Models;

namespace PasswordManagerApp.Repositories;

public interface IPasswordRepository
{
    Task<IEnumerable<PasswordEntry>> GetAllAsync(int userId);
    Task<PasswordEntry?> GetByIdAsync(int id, int userId);
    Task AddAsync(PasswordEntry password, int userId);
    Task UpdateAsync(PasswordEntry password, int userId);
    Task DeleteAsync(int id, int userId);
    Task<IEnumerable<Category>> GetCategoriesAsync();
}