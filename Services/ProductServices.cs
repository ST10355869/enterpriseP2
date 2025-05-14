using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.EntityFrameworkCore;

namespace enterpriseP2.Services
{
    public class ProductServices
    {
        private readonly AppDbContext _context;
        private readonly AuthenticateService _authService;

        public ProductServices(AppDbContext context,    AuthenticateService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task AddProduct(ProductModel product, int farmerId)
        {
            //Automatically set the date when the product is added
            product.DateAdded = DateOnly.FromDateTime(DateTime.Now);
            //assigns product to the farmer
            product.FarmerId = farmerId;
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductModel>> GetProductsByFarmer(int farmerId)
        {
            try
            {
                return await _context.Products
           .Where(p => p.FarmerId == farmerId)
           .ToListAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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