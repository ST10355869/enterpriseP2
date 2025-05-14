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
            // Check if login credentials are valid
            if (await _authService.Login(username, password))
            {
                // Redirect based on user role
                if (User.IsInRole("Employee"))
                {
                    return RedirectToAction("Index", "Farmer"); 
                }
                return RedirectToAction("Index", "ProductModels"); 
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