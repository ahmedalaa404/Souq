


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Souq.Api.Errors;
using Souq.Api.Extenstion;
using Souq.Api.Profilers;
using Souq.Core.DataBase;
using Souq.Core.Entites.Identity;
using Souq.Core.Repositories;
using Souq.Core.Services;
using Souq.Repositorey;
using Souq.Repositorey.DataBase;
using Souq.Repositorey.DataBase.DataSeed;
using Souq.Repositorey.DataBase.Identity;
using Souq.Repositorey.Repo;
using Souq.Services;
using Souq.Services.TokenServices;
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

            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddApplicationServices();

            builder.Services.AddSingleton<IConnectionMultiplexer>(o =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
               return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddScoped<IBasketRepo, BasketRepo>();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddScoped<ITokenServices,TokenServices>();
            builder.Services.AddScoped<IUniteOFWork, UniteOFWork>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();//perRequest
            builder.Services.AddScoped<IPaymentServices, PaymentServices>();//perRequest


            builder.Services.AddSingleton<IResponseCacheServices, ResponseCacheServices>(); // Per ReQuest Allow Dependecy Injection 

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("MyPolicey", x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );
            }
            );
            #endregion End Configurations





           var app = builder.Build();

            using var Scope = app.Services.CreateScope(); // Make Scope Manually  -- that`s not Under Controller CLR
            var Services = Scope.ServiceProvider;
            var loggerFactorey= Services.GetRequiredService<ILoggerFactory>(); // Allow Dependency Logger Factorey 
            #region Take Instance DbContext To Make Appliy Migration -- Update Database Explicety 
            try
            {
                var ContextInstance = Services.GetRequiredService<StoreContext>(); // ask CLr To Create instance from dbcontext Explicety
                var AppContextInstance = Services.GetRequiredService<AppIdentityDbContext>(); // ask CLr To Create instance from dbcontext Explicety
                await AppContextInstance.Database.MigrateAsync();
                await ContextInstance.Database.MigrateAsync(); //Must To Make Dispose The Connection

                await StoreContextSeed.SeedData(ContextInstance);
                var  Manager= Services.GetRequiredService<UserManager<AppUser>>();


                await AppIdentityDbcontextSeeding.SeedAccount(Manager);


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
            #region Authentication Authorization 
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseCors("MyPolicey");

            app.MapControllers();

            app.Run(); // Run Console Kestral WebApplications 
        }
    }
}
