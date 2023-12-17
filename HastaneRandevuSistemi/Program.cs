using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HastaneRandevuSistemi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HastaneRandevuSistemiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HastaneRandevuSistemiContext") ?? throw new InvalidOperationException("Connection string 'HastaneRandevuSistemiContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireRole("Hasta")
        .Build();
    options.AddPolicy("Doktor", policy => policy.RequireRole("Doktor"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(45);
});
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
