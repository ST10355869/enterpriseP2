using enterpriseP2.Data;
using enterpriseP2.Models;  

namespace enterpriseP2.Services
{
    public class AddProductService
    {
        private readonly AppDbContext _context;
        public AddProductService(AppDbContext context)
        {
            _context = context;
        }
        public Task AddProduct(ProductModel product)
        {
            _context.Products.Add(product);
            return _context.SaveChangesAsync();
        }
    }
}
