using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using PenguinCatcher.DataAccess;
using PenguinCatcher.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using PenguinCatcher.Controllers;
using PenguinCatcher.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PenguinContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PenguinContext")));

builder.Services.AddIdentity<PenguinCatcherUser, IdentityRole>().AddEntityFrameworkStores<PenguinContext>().AddDefaultTokenProviders();

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

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PenguinContext>().Database.EnsureCreated();
    await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
