using enterpriseP2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace enterpriseP2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticateService _authService;

        public AccountController(AuthenticateService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (await _authService.Login(username, password))
            {
                // Let the auth cookie fully process before redirect
                await Task.Delay(100); // Small delay
                return RedirectToAction("Index", "ProductModels");
            }
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}