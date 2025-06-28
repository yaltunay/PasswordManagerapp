
using Microsoft.EntityFrameworkCore;
using PasswordManagerApp.Data;
using PasswordManagerApp.Repositories;
using PasswordManagerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

// DI
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();