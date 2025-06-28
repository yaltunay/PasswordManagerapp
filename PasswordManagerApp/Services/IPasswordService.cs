using PasswordManagerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManagerApp.Services;

public interface IPasswordService
{
    Task<IEnumerable<PasswordEntry>> GetUserPasswordsAsync(int userId);
    Task<PasswordEntry?> GetPasswordByIdAsync(int id, int userId);
    Task AddPasswordAsync(PasswordEntry password, int userId);
    Task UpdatePasswordAsync(PasswordEntry password, int userId);
    Task DeletePasswordAsync(int id, int userId);
    Task<IEnumerable<Category>> GetCategoriesAsync();
}