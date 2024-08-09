using LicenseServerApp.Utils.Interfaces;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRefitClient<IApi>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:7260/");

}).SetHandlerLifetime(TimeSpan.FromHours(12));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
