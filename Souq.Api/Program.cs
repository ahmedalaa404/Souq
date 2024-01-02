


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Souq.Repositorey.DataBase;

namespace Souq.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Configurations 
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(option=>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
    #endregion


            var app = builder.Build();

            using var Scope = app.Services.CreateScope(); // Make Scope Manually  -- that`s not Under Controller CLR
            var Services = Scope.ServiceProvider;
            var loggerFactorey= Services.GetRequiredService<ILoggerFactory>(); // Allow Dependency Logger Factorey 
            #region Take Instance DbContext To Make Appliy Migration -- Update Database Explicety 
            try
            {
                var ContextInstance = Services.GetRequiredService<StoreContext>(); // ask CLr To Create instance from dbcontext Explicety
                await ContextInstance.Database.MigrateAsync(); //Must To Make Dispose The Connection

            }
            catch (Exception Errors)
            {
                var Logger = loggerFactorey.CreateLogger<Program>();
                Logger.LogError("This Error Happen When Make Migration",Errors.Message);
                //Console.WriteLine(Errors.Message);
                
            }



            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); // Run Console Kestral WebApplications 
        }
    }
}
