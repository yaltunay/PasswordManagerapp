using Microsoft.AspNetCore.Mvc;
using PasswordManagerApp.Models;
using PasswordManagerApp.Services;
using System.Threading.Tasks;

namespace PasswordManagerApp.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var userId = GetLoggedInUserId();
            if (userId == -1) return RedirectToAction("Login");

            var passwords = await _passwordService.GetUserPasswordsAsync(userId);

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                passwords = passwords.Where(p =>
                    p.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (p.Url?.Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)).ToList();
            }

            var categories = await _passwordService.GetCategoriesAsync();
            ViewBag.Categories = categories;

            return View(passwords);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _passwordService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PasswordEntry model)
        {
            var userId = GetLoggedInUserId();
            if (userId == -1) return RedirectToAction("Login");

            await _passwordService.AddPasswordAsync(model, userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetLoggedInUserId();
            var password = await _passwordService.GetPasswordByIdAsync(id, userId);

            if (password == null) return NotFound();

            var categories = await _passwordService.GetCategoriesAsync();
            ViewBag.Categories = categories;

            return View(password);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PasswordEntry model)
        {
            var userId = GetLoggedInUserId();
            var existing = await _passwordService.GetPasswordByIdAsync(model.Id, userId);

            if (existing == null) return NotFound();

            await _passwordService.UpdatePasswordAsync(model, userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetLoggedInUserId();
            await _passwordService.DeletePasswordAsync(id, userId);
            return RedirectToAction("Index");
        }

        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? -1;
        }
    }
}