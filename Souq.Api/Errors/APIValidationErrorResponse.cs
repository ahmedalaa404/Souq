namespace Souq.Api.Errors
{
    public class APIValidationErrorResponse : ApiResponse
    {
        public IList<string> Errors { get; set; }
        public APIValidationErrorResponse( string? ErrorMessage = null, IList<string> errors = null) : base(400, ErrorMessage)
        {
            Errors = errors;
        }
    }
}
