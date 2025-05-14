using enterpriseP2.Data;
using enterpriseP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Farmer")]
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
        if (User.IsInRole("Farmer"))
        {
            var farmers = _context.Farmers.ToList();
            return View(farmers);
        }
        return RedirectToAction("Login", "Account");
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
    public IActionResult Products(int farmerId)
    {
        var products = _context.Products
            .Where(p => p.FarmerId == farmerId)
            .ToList();

        ViewBag.FarmerName = _context.Farmers
            .FirstOrDefault(f => f.Id == farmerId)?.FirstName;

        return View(products);
    }
}