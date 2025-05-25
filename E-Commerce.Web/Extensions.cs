using System.Text;
using Domain.Contracts;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Authentication;

namespace E_Commerce.Web
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerServices
            (this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type=ReferenceType.SecurityScheme,
                             Id="Bearer"
                         }
                     },
                     new List<string>()
                 }
             });
            });
            return services;
        }
        public static IServiceCollection AddWebApplicationServices
    (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(); /* .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)*/
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //Func<ActionContext,IActionResult>
                options.InvalidModelStateResponseFactory = ApiResponseFactory
                .GenerateApiValidationResponse;
            });
            services.AddSwaggerServices();
            ConfigureJwt(services, configuration);
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
            await dbInitializer.InitializeIdentityAsync();
            return app;
        
        }

        private static void ConfigureJwt(IServiceCollection services,IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer= jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
                };
            });


            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("User", policy => policy.RequireRole("User"));
            //});
        }
    };


}
