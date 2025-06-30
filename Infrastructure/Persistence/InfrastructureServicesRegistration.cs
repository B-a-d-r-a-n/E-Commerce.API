







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
            services.AddScoped<ICashRepository, CashRepository>();
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
