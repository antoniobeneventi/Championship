using Championship;
using ChampionshipWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


public class AccountController : Controller
{
    private readonly FootballLeagueContext _context;

    public AccountController(FootballLeagueContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) 
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }

        ViewData["Username"] = username;
        ModelState.AddModelError(string.Empty, "Tentativo di login non valido.");
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                ModelState.AddModelError(string.Empty, "Nome utente già esistente.");
                return RedirectToAction("Login");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Password = hashedPassword 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }
        return RedirectToAction("Login");
    }

}


