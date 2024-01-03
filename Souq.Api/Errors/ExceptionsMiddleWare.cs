using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Souq.Api.Errors
{
    public class ExceptionsMiddleWare //ClR Is Run
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionsMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionsMiddleWare(RequestDelegate Next,ILogger<ExceptionsMiddleWare>  Logger,IHostEnvironment Env)
        {
            next = Next;
            logger = Logger;
            env = Env;
        }


        public async Task InvokeAsync(HttpContext Context)  
        {
            try
            {
                await next.Invoke(Context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                //Log Exception In DataBase
                Context.Response.ContentType = "application/json";

                Context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;

                if(env.IsDevelopment() )
                {
                    var Response = new ApiExceptionResponse((int)StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString());
                    Context.Response.WriteAsync(JsonSerializer.Serialize(Response));

                }
                else
                {
                    var Response = new ApiExceptionResponse((int)StatusCodes.Status500InternalServerError);

                }

         
            }

        }










    }
}
