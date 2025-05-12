using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.EntityFrameworkCore;

namespace enterpriseP2.Services
{
    public class ProductServices
    {
        private readonly AppDbContext _context;
        private readonly AuthenticationService _authService;

        public ProductServices(AppDbContext context, AuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task AddProduct(ProductModel product, int farmerId)
        {
            product.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            product.FarmerId = farmerId;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductModel>> GetProductsByFarmer(int farmerId)
        {
            return await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();
        }

        public async Task ShowProducts()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Name}, Category: {product.Category}, Price: {product.Price}, Date Added: {product.DateAdded}");
            }
        }
    }
}