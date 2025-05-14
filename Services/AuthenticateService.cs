using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace enterpriseP2.Services
{
    public class AuthenticateService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> Login(string username, string password)
        {
            var farmer = await _context.Farmers
        .FirstOrDefaultAsync(f => f.Username == username && f.Password == password);

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Username == username && e.Password == password);

            if (farmer == null && employee == null)
                return false;

            var claims = new List<Claim>();

            if (farmer != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, farmer.Username));
                claims.Add(new Claim("UserId", farmer.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, "Farmer")); 
            }
            else if (employee != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, employee.Username));
                claims.Add(new Claim("UserId", employee.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, "Employee")); 
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return true;
        }


        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        public bool IsFarmer()
        {
            return GetCurrentUserRole() == "Farmer";
        }

        public bool IsEmployee()
        {
            return GetCurrentUserRole() == "Employee";
        }

        public int? GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }

        public string? GetCurrentUserRole()
{
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        }

    }
}