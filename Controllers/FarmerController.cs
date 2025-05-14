using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Employee")]
public class FarmerController : Controller
{
    private readonly AppDbContext _context;

    public FarmerController(AppDbContext context)
    {
        _context = context;
    }

   
    public IActionResult Index()
    {
        var farmers = _context.Farmers.ToList();
        return View("~/Views/Farmer/Index.cshtml", farmers);
    }
   
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(FarmerModel farmer)
    {
        if (ModelState.IsValid)
        {
            farmer.Role = "Farmer";
            _context.Farmers.Add(farmer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(farmer);
    }

    
    public IActionResult Products(int farmerId, string categoryFilter = "",
                            int? year = null, int? month = null)
    {
        // Check if farmer exists
        var farmer = _context.Farmers.FirstOrDefault(f => f.Id == farmerId);
        if (farmer == null)
        {
            return NotFound();
        }

        IQueryable<ProductModel> query = _context.Products
            .Where(p => p.FarmerId == farmerId);

       
        if (!string.IsNullOrEmpty(categoryFilter))
        {
            query = query.Where(p => p.Category.ToLower() == categoryFilter.ToLower());
        }

        
        if (year.HasValue)
        {
            if (month.HasValue)
            {
               
                query = query.Where(p => p.DateAdded.Year == year.Value &&
                                       p.DateAdded.Month == month.Value);
            }
            else
            {
              
                query = query.Where(p => p.DateAdded.Year == year.Value);
            }
        }

       
        query = query.OrderByDescending(p => p.DateAdded);

        var products = query.ToList();

        ViewBag.FarmerName = $"{farmer.FirstName} {farmer.LastName}";
        ViewBag.CurrentFilter = categoryFilter;
        ViewBag.SelectedYear = year;
        ViewBag.SelectedMonth = month;

      
        ViewBag.AvailableYears = _context.Products
            .Where(p => p.FarmerId == farmerId)
            .Select(p => p.DateAdded.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();

        return View(products);
    }
}