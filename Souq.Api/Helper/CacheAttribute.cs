using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Souq.Core.Services;

namespace Souq.Api.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int time;
 

        public CacheAttribute(int time)
        {
            this.time = time;
           
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create With Dependency Injection 

            var CacheServices = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServices>();

            var CacheKeyFromReqest = GenerateCasheKey(context.HttpContext.Request);

            var CacheResponse = await CacheServices.GetCachedResponseAsync(CacheKeyFromReqest);

            if(!string.IsNullOrEmpty(CacheResponse)) 
            {
                var ContextResulte = new ContentResult()
                {
                    Content = CacheResponse,
                    ContentType = "application/json"
                    ,
                    StatusCode = 200


                };

                context.Result=ContextResulte;
                return;
            }


          var ExcutedEndPointContext=  await next.Invoke();








        }

        private string GenerateCasheKey(HttpRequest request)
        {





            
        }
    }
}
