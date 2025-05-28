global using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using Domain.Models.Identity;
using E_Commerce.Web.Factories;
using E_Commerce.Web.MiddleWares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstraction;
using Shared.DataTransferObjects.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web
{
    public class Program
    {
        public  static  async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            //builder.Logging.AddEventLog();

            // Add services to the container.
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



            var app = builder.Build();
           
            await app.InitializeDataBaseAsync(); //await InitializeDbAsync(app);
            app.UseCustomExceptionMiddleware();      //app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "E-Commerce API";
                    options.DocExpansion(DocExpansion.None);

                    //options.InjectStylesheet();

                    options.EnableFilter();
                    options.DisplayRequestDuration();
                });
                
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        
    }
}
