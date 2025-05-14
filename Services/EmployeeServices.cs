using enterpriseP2.Data;
using enterpriseP2.Models;
using enterpriseP2.Services;
using Microsoft.EntityFrameworkCore;

namespace enterpriseP2.Services
{
    public class EmployeeServices
    {
        private readonly AppDbContext _context;
        private readonly AuthenticateService _authService;

        public EmployeeServices(AppDbContext context, AuthenticateService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<bool> CreateFarmer(FarmerModel farmer)
        {
            try
            {
                // Validate username
                if (string.IsNullOrEmpty(farmer.Username))
                    return false;

                // Check if username already exists
                if (await _context.Farmers.AnyAsync(f => f.Username == farmer.Username))
                    return false;

                farmer.Role = "Farmer"; // Ensure role is set
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<ProductModel>> ListFarmers(int farmerId)
        {
            return await _context.Products
                  .Where(p => p.FarmerId == farmerId)
                  .ToListAsync();
        }
        public async Task<List<ProductModel>> GetFarmerProducts(int farmerId)
        {
            return await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();
        }
        public async Task<List<FarmerModel>> GetAllFarmers()
        {
            return await _context.Farmers.ToListAsync();
        }
    }
}
