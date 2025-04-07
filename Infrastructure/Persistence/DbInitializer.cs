
using System.Text.Json;
using Domain.Contracts;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer(StoreDbContext context) : IDbInitializer
    {
        // Production => Create Db , Seeding [Context]

        // Check to see if there is any Pending Migrations
        //if (!await context.Database.GetPendingMigrationsAsync().Any())
        //{
        // Apply Migration
        //    await context.Database.MigrateAsync();
        //}

        // Dev => Seeding [Context]
        public async Task InitializeAsync()
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    // Read from the file
                    var BrandsData = await File.ReadAllTextAsync(path: @"..\..\Persistence\Data\Seeding\brands.json");

                    // Deserialize => Convert from String to C# Object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(json: BrandsData);

                    if (brands != null && brands.Any())
                    {
                        context.ProductBrands.AddRange(entities: brands);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ProductBrands.Any())
                {
                    // Read from the file
                    var TypesData = await File.ReadAllTextAsync(path: @"..\..\Persistence\Data\Seeding\brands.json");

                    // Deserialize => Convert from String to C# Object
                    var types = JsonSerializer.Deserialize<List<ProductType>>(json: TypesData);

                    if (types != null && types.Any())
                    {
                        context.ProductTypes.AddRange(entities: types);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    // Read from the file
                    var productsData = await File.ReadAllTextAsync(path: @"..\..\Persistence\Data\Seeding\products.json");

                    // Deserialize => Convert from String to C# Object
                    var products = JsonSerializer.Deserialize<List<Product>>(json: productsData);

                    if (products != null && products.Any())
                    {
                        context.Products.AddRange(entities: products);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
