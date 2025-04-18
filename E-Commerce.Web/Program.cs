global using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using E_Commerce.Web.Factories;
using E_Commerce.Web.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstraction;
using Shared.DataTransferObjects.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public  static  async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

              
            builder.Services.AddControllers(); /* .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)*/
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebApplicationServices();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



            var app = builder.Build();
           
            await app.InitializeDataBaseAsync(); //await InitializeDbAsync(app);
            app.UseCustomExceptionMiddleware();      //app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        
    }
}
