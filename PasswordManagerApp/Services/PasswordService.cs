using PasswordManagerApp.Models;
using PasswordManagerApp.Repositories;
using PasswordManagerApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManagerApp.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordRepository _repository;

    public PasswordService(IPasswordRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PasswordEntry>> GetUserPasswordsAsync(int userId)
    {
        return await _repository.GetAllAsync(userId);
    }

    public async Task<PasswordEntry?> GetPasswordByIdAsync(int id, int userId)
    {
        return await _repository.GetByIdAsync(id, userId);
    }

    public async Task AddPasswordAsync(PasswordEntry password, int userId)
    {
        await _repository.AddAsync(password, userId);
    }

    public async Task UpdatePasswordAsync(PasswordEntry password, int userId)
    {
        await _repository.UpdateAsync(password, userId);
    }

    public async Task DeletePasswordAsync(int id, int userId)
    {
        await _repository.DeleteAsync(id, userId);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _repository.GetCategoriesAsync();
    }
}