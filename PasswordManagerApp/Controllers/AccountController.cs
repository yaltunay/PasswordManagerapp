using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Data;
using PasswordManagerApp.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerApp.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null && user.PasswordHash == HashPassword(password))
        {
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Password");
        }

        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password, string passwordConfirm)
    {
        if (password != passwordConfirm)
        {
            ModelState.AddModelError("password", "Şifreler eşleşmiyor.");
            return View();
        }

        if (await _context.Users.AnyAsync(u => u.Username == username))
        {
            ModelState.AddModelError("username", "Bu kullanıcı adı zaten alınmış.");
            return View();
        }

        var user = new User
        {
            Username = username,
            PasswordHash = HashPassword(password)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}