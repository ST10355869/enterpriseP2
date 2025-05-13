using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using enterpriseP2.Models;


namespace enterpriseP2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<FarmerModel> Farmers { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }

    }

}
