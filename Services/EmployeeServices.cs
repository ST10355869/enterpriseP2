using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.EntityFrameworkCore;

namespace enterpriseP2.Services
{
    public class EmployeeServices
    {
        private readonly AppDbContext _context;
        private readonly AuthenticationService _authService;

        public EmployeeServices(AppDbContext context, AuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<List<ProductModel>> GetProductsByFarmer(int farmerId)
        {
            return await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();
        }
    }
}
