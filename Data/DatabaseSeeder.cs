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
                return; 
            }

            // Seed 1 Employee
            var employee = new EmployeeModel
            {
                FirstName = "Admin",
                LastName = "User",
                Username = "admin",
                Password = "admin123", 
                Role = "Employee"
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            // Seed 2 Farmers
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
                }
            };

            foreach (var farmer in farmers)
            {
                context.Farmers.Add(farmer);
            }
            context.SaveChanges();

            
            var farmerIds = context.Farmers.Select(f => f.Id).ToList();

         
            int currentYear = DateTime.Now.Year;

           
            foreach (var farmerId in farmerIds)
            {
                
                if (farmerId == farmerIds[0])
                {
                    context.Products.Add(new ProductModel
                    {
                        Name = "Grass-fed Beef",
                        Category = "protein",
                        Price = 12.99,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 3, 15)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Organic Quinoa",
                        Category = "grain",
                        Price = 5.99,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 6, 20)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Fresh Apples",
                        Category = "fruit",
                        Price = 3.49,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 9, 10)),
                        FarmerId = farmerId
                    });
                }
               
                else if (farmerId == farmerIds[1])
                {
                    context.Products.Add(new ProductModel
                    {
                        Name = "Free-range Eggs",
                        Category = "protein",
                        Price = 4.99,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 4, 5)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Whole Wheat Flour",
                        Category = "grain",
                        Price = 3.29,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 7, 15)),
                        FarmerId = farmerId
                    });

                    context.Products.Add(new ProductModel
                    {
                        Name = "Organic Carrots",
                        Category = "vegetable",
                        Price = 2.99,
                        DateAdded = DateOnly.FromDateTime(new DateTime(currentYear, 10, 20)),
                        FarmerId = farmerId
                    });
                }
            }

            context.SaveChanges();
        }
    }
}