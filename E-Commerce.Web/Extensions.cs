using Domain.Contracts;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerServices
            (this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddWebApplicationServices
    (this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //Func<ActionContext,IActionResult>
                options.InvalidModelStateResponseFactory = ApiResponseFactory
                .GenerateApiValidationResponse;
            });
            services.AddSwaggerServices();
            return services;
        }


        public static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            
        
            // Object from type that Implements IDbInitializer
            // Create Scope to Resolve the Scoped Service

            using var scope = app.Services.CreateScope(); // UnManaged *using -> auto dispose*
                                                          // Resolve from the Scope
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        
        }
    }
}
