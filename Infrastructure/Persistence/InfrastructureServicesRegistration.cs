
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                var ConnectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConnectionString);
            }); 
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var ConnectionString = configuration.GetConnectionString("IdentityConnection");
                options.UseSqlServer(ConnectionString);
            });
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
              return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            ConfigureIdentity(services, configuration);
            return services;
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<ApplicationUser>(config =>
            {
                //config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedAccount = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
        }
    }
}
