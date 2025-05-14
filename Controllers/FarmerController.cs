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

    // Employees can view all farmers
    public IActionResult Index()
    {
        var farmers = _context.Farmers.ToList();
        return View("~/Views/Farmer/Index.cshtml", farmers);
    }
    // Employees can create new farmers
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

    // Employees can view any farmer's products
    public IActionResult Products(int farmerId, string categoryFilter = "",
                             int? year = null, int? month = null)
    {
        var farmer = _context.Farmers.FirstOrDefault(f => f.Id == farmerId);
        if (farmer == null)
        {
            return NotFound();
        }

        IQueryable<ProductModel> query = _context.Products
            .Where(p => p.FarmerId == farmerId);

        // Apply category filter if specified
        if (!string.IsNullOrEmpty(categoryFilter))
        {
            query = query.Where(p => p.Category.ToLower() == categoryFilter.ToLower());
        }

        // Apply date range filter if specified
        if (year.HasValue && month.HasValue)
        {
            var startDate = new DateOnly(year.Value, month.Value, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            query = query.Where(p => p.DateAdded >= startDate && p.DateAdded <= endDate);
        }

        // Default sorting by date (newest first)
        query = query.OrderByDescending(p => p.DateAdded);

        var products = query.ToList();

        ViewBag.FarmerName = $"{farmer.FirstName} {farmer.LastName}";
        ViewBag.CurrentFilter = categoryFilter;
        ViewBag.SelectedYear = year;
        ViewBag.SelectedMonth = month;

        // Get distinct years with products for dropdown
        ViewBag.AvailableYears = _context.Products
            .Where(p => p.FarmerId == farmerId)
            .Select(p => p.DateAdded.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();

        return View(products);
    }
}