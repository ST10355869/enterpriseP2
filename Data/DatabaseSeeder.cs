using enterpriseP2.Models;
using System;

namespace enterpriseP2.Data
{
    public class DatabaseSeeder
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.Employees.Any() || context.Farmers.Any() || context.Products.Any())
            {
                return; // Database has been seeded already
            }

            // Seed 1 Employee
            var employee = new EmployeeModel
            {
                FirstName = "Admin",
                LastName = "User",
                Username = "admin",
                Password = "admin123", // Note: In production, hash passwords!
                Role = "Employee"
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            // Seed 3 Farmers
            var farmers = new FarmerModel[]
            {
                new FarmerModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "farmer1",
                    Password = "farmer123",
                    Role = "Farmer"
                },
                new FarmerModel
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Username = "farmer2",
                    Password = "farmer456",
                    Role = "Farmer"
                },
                new FarmerModel
                {
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Username = "farmer3",
                    Password = "farmer789",
                    Role = "Farmer"
                }
            };

            foreach (var farmer in farmers)
            {
                context.Farmers.Add(farmer);
            }
            context.SaveChanges();

            // Get the farmer IDs after they've been saved
            var farmerIds = context.Farmers.Select(f => f.Id).ToList();

            // Seed products for each farmer (3 products per farmer)
            var random = new Random();
            string[] categories = { "protein", "grain", "fruit", "vegetable" };

            foreach (var farmerId in farmerIds)
            {
                // Farmer 1 products
                context.Products.Add(new ProductModel
                {
                    Name = "Grass-fed Beef",
                    Category = "protein",
                    Price = 12.99,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                    FarmerId = farmerId
                });

                context.Products.Add(new ProductModel
                {
                    Name = "Organic Quinoa",
                    Category = "grain",
                    Price = 5.99,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    FarmerId = farmerId
                });

                context.Products.Add(new ProductModel
                {
                    Name = "Fresh Apples",
                    Category = "fruit",
                    Price = 3.49,
                    DateAdded = DateOnly.FromDateTime(DateTime.Now),
                    FarmerId = farmerId
                });

                // For variety, you could make the products different for each farmer
                if (farmerId == farmerIds[1]) // Second farmer
                {
                    context.Products.Add(new ProductModel
                    {
                        Name = "Free-range Eggs",
                        Category = "protein",
                        Price = 4.99,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Whole Wheat Flour",
                        Category = "grain",
                        Price = 3.29,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Organic Carrots",
                        Category = "vegetable",
                        Price = 2.99,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now),
                        FarmerId = farmerId
                    });
                }
                else if (farmerId == farmerIds[2]) // Third farmer
                {
                    context.Products.Add(new ProductModel
                    {
                        Name = "Organic Chicken",
                        Category = "protein",
                        Price = 8.99,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Brown Rice",
                        Category = "grain",
                        Price = 4.49,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Fresh Strawberries",
                        Category = "fruit",
                        Price = 5.99,
                        DateAdded = DateOnly.FromDateTime(DateTime.Now),
                        FarmerId = farmerId
                    });
                }
            }

            context.SaveChanges();
        }
    }
}