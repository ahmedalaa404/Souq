namespace Souq.Api.Extenstion
{
    public static  class SwaggerConfiguration
    {




        public  static IServiceCollection Swaggers(this IServiceCollection Services)
        {
            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }
    }
}
