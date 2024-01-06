


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Souq.Api.Errors;
using Souq.Api.Extenstion;
using Souq.Api.Profilers;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Repositorey.DataBase;
using Souq.Repositorey.DataBase.DataSeed;
using Souq.Repositorey.Repo;
using StackExchange.Redis;

namespace Souq.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Configurations 
            // Add services to the container.

            #region Configuration Swagger
            builder.Services.Swaggers();
            #endregion



            builder.Services.AddDbContext<StoreContext>(option=>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddSingleton<IConnectionMultiplexer>(o =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
               return ConnectionMultiplexer.Connect(Connection);
            });
            #endregion End Configurations





            var app = builder.Build();

            using var Scope = app.Services.CreateScope(); // Make Scope Manually  -- that`s not Under Controller CLR
            var Services = Scope.ServiceProvider;
            var loggerFactorey= Services.GetRequiredService<ILoggerFactory>(); // Allow Dependency Logger Factorey 
            #region Take Instance DbContext To Make Appliy Migration -- Update Database Explicety 
            try
            {
                var ContextInstance = Services.GetRequiredService<StoreContext>(); // ask CLr To Create instance from dbcontext Explicety
                await ContextInstance.Database.MigrateAsync(); //Must To Make Dispose The Connection

                await StoreContextSeed.SeedData(ContextInstance);


            }
            catch (Exception Errors)
            {
                var Logger = loggerFactorey.CreateLogger<Program>();
                Logger.LogError("This Error Happen When Make Migration",Errors.Message);
                //Console.WriteLine(Errors.Message);
                
            }



            #endregion





            app.UseMiddleware<ExceptionsMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); // Run Console Kestral WebApplications 
        }
    }
}
