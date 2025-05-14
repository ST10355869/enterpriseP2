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
    public IActionResult Products(int farmerId, string categoryFilter = "", string dateSortOrder = "")
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

        // Apply date sorting
        switch (dateSortOrder?.ToLower())
        {
            case "newest":
                query = query.OrderByDescending(p => p.DateAdded);
                break;
            case "oldest":
                query = query.OrderBy(p => p.DateAdded);
                break;
            default:
                // Default sorting (you can change this)
                query = query.OrderByDescending(p => p.DateAdded);
                break;
        }

        var products = query.ToList();

        ViewBag.FarmerName = $"{farmer.FirstName} {farmer.LastName}";
        ViewBag.CurrentFilter = categoryFilter;
        ViewBag.DateSortOrder = dateSortOrder;

        return View(products);
    }
}