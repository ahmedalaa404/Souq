


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Souq.Api.Errors;
using Souq.Api.Profilers;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Repositorey.DataBase;
using Souq.Repositorey.DataBase.DataSeed;
using Souq.Repositorey.Repo;

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



            builder.Services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            builder.Services.AddAutoMapper(typeof(MapperDto).Assembly ); // to Allow Dependency injection



            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var Errors = ActionContext.ModelState.Where(X => X.Value.Errors.Count() > 0);
                    var RealErrors = Errors.SelectMany(X => X.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    var ValidationErrorsResponse = new APIValidationErrorResponse()
                    {
                        Errors = RealErrors,
                    };
                    return new BadRequestObjectResult(ValidationErrorsResponse);

                };

            });


            builder.Services.Configure<ApiBehaviorOptions>(X =>
            {
                X.SuppressModelStateInvalidFilter = true;
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); // Run Console Kestral WebApplications 
        }
    }
}
