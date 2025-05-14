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

        public async Task<List<ProductModel>> ListFarmers(int farmerId)
        {
            return await _context.Products
                  .Where(p => p.FarmerId == farmerId)
                  .ToListAsync();
        }

    }
}
