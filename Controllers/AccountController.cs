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
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (await _authService.Login(username, password))
            {
                // Check role and redirect appropriately
                if (User.IsInRole("Employee"))
                {
                    return RedirectToAction("Index", "Farmer"); 
                }
                return RedirectToAction("Index", "ProductModels"); // Farmers go to products
            }
            ViewBag.Error = "Invalid username or password";
            return View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}