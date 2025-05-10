using enterpriseP2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace enterpriseP2.Models
{
    public class FarmerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }


public static class FarmerModelEndpoints
{
	public static void MapFarmerModelEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/FarmerModel").WithTags(nameof(FarmerModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Farmer.ToListAsync();
        })
        .WithName("GetAllFarmerModels")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<FarmerModel>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.Farmer.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is FarmerModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFarmerModelById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, FarmerModel farmerModel, AppDbContext db) =>
        {
            var affected = await db.Farmer
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, farmerModel.Id)
                  .SetProperty(m => m.FirstName, farmerModel.FirstName)
                  .SetProperty(m => m.LastName, farmerModel.LastName)
                  .SetProperty(m => m.Username, farmerModel.Username)
                  .SetProperty(m => m.Password, farmerModel.Password)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFarmerModel")
        .WithOpenApi();

        group.MapPost("/", async (FarmerModel farmerModel, AppDbContext db) =>
        {
            db.Farmer.Add(farmerModel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/FarmerModel/{farmerModel.Id}",farmerModel);
        })
        .WithName("CreateFarmerModel")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.Farmer
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFarmerModel")
        .WithOpenApi();
    }
}}
