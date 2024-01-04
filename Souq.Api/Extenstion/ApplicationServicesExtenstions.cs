using Microsoft.AspNetCore.Mvc;
using Souq.Api.Errors;
using Souq.Api.Profilers;
using Souq.Core.Repositories;
using Souq.Repositorey.Repo;

namespace Souq.Api.Extenstion
{
    public static class ApplicationServicesExtenstions
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            builder.AddAutoMapper(typeof(MapperDto).Assembly); // to Allow Dependency injection



            builder.Configure<ApiBehaviorOptions>(options =>
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


            builder.Configure<ApiBehaviorOptions>(X =>
            {
                X.SuppressModelStateInvalidFilter = true;
            });


            return builder; 

        }





    }
}
