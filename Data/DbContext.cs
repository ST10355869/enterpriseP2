using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using enterpriseP2.Models;


namespace enterpriseP2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<FarmerModel> Farmer { get; set; }
    }
    
}
