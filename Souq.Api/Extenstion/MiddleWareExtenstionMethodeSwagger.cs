namespace Souq.Api.Extenstion
{
    public static class MiddleWareExtenstionMethodeSwagger
    {


        public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

    }
}
