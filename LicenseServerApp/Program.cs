using LicenseServerApp.Utils.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRefitClient<IApi>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://localhost:7260/");

    //c.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJpdmFuMTIzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJqdGkiOiI4NzI1OTZlNC1iYzViLTQzMTMtYTI1ZC05NmQ4ZDdiOGUxODIiLCJleHAiOjE3MjMxNjIwNjUsImlzcyI6Imlzc3VlciIsImF1ZCI6Imlzc3VlciJ9.r_mx3pVctiiXNgKavUajKFTFeXvH3gC-rNGmCza-si8");
}).SetHandlerLifetime(TimeSpan.FromHours(12));

/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "issuer",
        ValidAudience = "issuer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5674585686575768456769735897364579869805675896705986725-467859206798576"))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Authorization"];
            return Task.CompletedTask;
        }
    };
});*/

//builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options => {
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
// Middleware
app.UseRouting();
    
    /*
                  
app.UseAuthentication();
app.UseAuthorization();*/

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
