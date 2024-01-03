namespace Souq.Api.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        private readonly string? details;

        public ApiExceptionResponse(int Number, string? ErrorMessage = null, string ? Details=null) : base(Number, ErrorMessage)
        {
            details = Details;
        }
    }
}
