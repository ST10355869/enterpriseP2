using enterpriseP2.Data;
using enterpriseP2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace enterpriseP2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticateService _authService;
        private readonly ILogger<AccountController> _logger;
        private readonly AppDbContext _context;

        public AccountController(AuthenticateService authService, ILogger<AccountController> logger, AppDbContext context)
        {
            _authService = authService;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(); // Use your service if needed
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            _logger.LogInformation($"Login attempt for: {username}");

            if (await _authService.Login(username, password))
            {
                _logger.LogInformation("Login successful");

                // Check if it's an employee first
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == username);
                if (employee != null)
                {
                    _logger.LogInformation("Redirecting to Farmer/Index");
                    return RedirectToAction("Index", "Farmer");
                }

                // If not employee, check if farmer
                var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Username == username);
                if (farmer != null)
                {
                    _logger.LogInformation("Redirecting to ProductModels/Index");
                    return RedirectToAction("Index", "ProductModels");
                }
            }

            _logger.LogWarning("Login failed");
            ViewBag.Error = "Invalid username or password";
            return View();
        }
    }
}