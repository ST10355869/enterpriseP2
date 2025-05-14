using enterpriseP2.Data;
using enterpriseP2.Models;
using enterpriseP2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize(Roles = "Farmer")]
public class ProductModelsController : Controller
{
    private readonly AppDbContext _context;
    private readonly AuthenticateService _authService;
    private readonly ProductServices _productService;

    public ProductModelsController(AppDbContext context, AuthenticateService authService, ProductServices productService)
    {
        _context = context;
        _authService = authService;
        _productService = productService;
    }


    public async Task<IActionResult> Index()
    {
        var currentUserId = User.FindFirstValue("UserId");
        if (!string.IsNullOrEmpty(currentUserId))
        {
            var products = await _context.Products
                .Where(p => p.FarmerId == int.Parse(currentUserId))
                .ToListAsync();
            return View(products);
        }
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Create()
    {
        return View(); // No need for check - handled by Authorize attribute
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            var farmerId = User.FindFirstValue("UserId");
            if (!string.IsNullOrEmpty(farmerId))
            {
                await _productService.AddProduct(product, int.Parse(farmerId));
                return RedirectToAction("Index");
            }
        }
        return View(product);
    }
}