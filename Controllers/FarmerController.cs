using enterpriseP2.Data;
using Microsoft.AspNetCore.Mvc;

namespace enterpriseP2.Controllers
{
    public class FarmerController : Controller
    {
        private readonly AppDbContext _context;

        public FarmerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get all users where role is Farmer
            var farmers = _context.Farmers.ToList(); // Assuming your DbSet is named 'Farmers'
            return View(farmers);
        }
    }
}
