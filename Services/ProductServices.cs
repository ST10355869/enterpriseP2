using enterpriseP2.Data;
using enterpriseP2.Models;  
using System.Threading.Tasks;

namespace enterpriseP2.Services
{
    public class ProductServices
    {
        private readonly AppDbContext _context;
        public ProductServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProduct(ProductModel product)
        {
            // Set the DateAdded property to the current date
            product.DateAdded = DateOnly.FromDateTime(DateTime.Now);    
           
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
