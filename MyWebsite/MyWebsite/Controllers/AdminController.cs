using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Helpers;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController(DatabaseContext context) =>
            Startup.Database = context;

        public IActionResult Index() =>
            View();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login() =>
            View();

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Authorization error", "Invalid data");
                return View(model);
            }

            LoginModel user = Startup.Database.Users.FirstOrDefault(i => i.Email == model.Email);
            if (user == null || !Cryptography.VerifyHash(model.Password, "SHA512", user.Password))
            {
                ModelState.AddModelError("Authorization error", "Invaild e-mail or password");
                return View(model);
            }

            Claim claim = new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email);

            ClaimsIdentity id = new ClaimsIdentity(new Claim[] { claim }, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }
    }
}