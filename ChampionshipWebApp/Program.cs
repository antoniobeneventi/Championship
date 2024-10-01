using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization; // Required for localization
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Configure the DbContext
builder.Services.AddDbContext<FootballLeagueContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FootballLeagueDatabase")));

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure request localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("it")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // Set culture based on cookie
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

// Add services for controllers with views and enable view localization
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(); // Enable localization middleware
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();

// Modify the default route to point to the login page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();








