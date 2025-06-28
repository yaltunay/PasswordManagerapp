namespace PasswordManagerApp.Models;

public class PasswordEntry
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public int UserId { get; set; }
}