
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class DbInitializer(StoreDbContext context,
        StoreIdentityDbContext identityDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager) 
        : IDbInitializer
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
                /// production => Create Db + Seeding
                /// Dev => Seeding
                 if ((await context.Database.GetPendingMigrationsAsync()).Any())
                 await context.Database.MigrateAsync();
                if (!context.Set<DeliveryMethod>().Any())
                {
                    // Read from the file
                    var DeliveryData = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Seeding","delivery.json"));

                    // Deserialize => Convert from String to C# Object
                    var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(json: DeliveryData);

                    if (delivery != null && delivery.Any())
                    {
                        context.Set<DeliveryMethod>().AddRange(entities: delivery);
                    }
                }
                    await context.SaveChangesAsync();
                    if (!context.Set<ProductBrand>().Any())
                {
                    // Read from the file
                    var BrandsData = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Seeding", "brands.json"));

                    // Deserialize => Convert from String to C# Object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(json: BrandsData);

                    if (brands != null && brands.Any())
                    {
                        context.Set<ProductBrand>().AddRange(entities: brands);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Set<ProductType>().Any())
                {
                    // Read from the file
                    var typesData = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Seeding", "types.json"));
                    // Deserialize => Convert from String to C# Object
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types != null && types.Any())
                    {
                        context.Set<ProductType>().AddRange(types);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Set<Product>().Any())
                {
                    // Read from the file
                    var productsData = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Seeding", "products.json"));
                    // Deserialize => Convert from String to C# Object
                    var products = JsonSerializer.Deserialize<List<Product>>(json: productsData);

                    if (products != null && products.Any())
                    {
                        context.Set<Product>().AddRange(entities: products);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!userManager.Users.Any())
            {
                var superAdminUser = new ApplicationUser
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "8123465789",
                };
                var adminUser = new ApplicationUser
                {
                    DisplayName= "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "8123465789",
                };
                await userManager.CreateAsync(superAdminUser, "Passw@rd");
                await userManager.CreateAsync(adminUser, "Passw@rd");
                await userManager.AddToRoleAsync(user: superAdminUser, role: "SuperAdmin");
                await userManager.AddToRoleAsync(user: adminUser, role: "Admin");
            }
        }
    }
}
