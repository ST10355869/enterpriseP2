using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.EntityFrameworkCore;

namespace enterpriseP2.Services
{
    public class AuthenticationService 
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(string username, string password)
        {
            var farmer = await _context.Farmers
                .FirstOrDefaultAsync(f => f.Username == username && f.Password == password);

            if (farmer != null)
            {
                _httpContextAccessor.HttpContext.Session.SetInt32("FarmerId", farmer.Id);
                _httpContextAccessor.HttpContext.Session.SetString("FarmerRole", farmer.Role);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public bool IsFarmer()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("FarmerRole") == "Farmer";
        }


        public int? GetCurrentFarmerId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("FarmerId");
        }
    }
}