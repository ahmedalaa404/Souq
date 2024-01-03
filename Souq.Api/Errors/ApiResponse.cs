using Microsoft.AspNetCore.Http;

namespace Souq.Api.Errors
{
    public class ApiResponse
    {


        public int StatusCode { get; set; }

        public string Message { get; set; }



        public ApiResponse(int Number, string? ErrorMessage)
        {
            StatusCode = Number;
            Message = !(string.IsNullOrEmpty(ErrorMessage))? ErrorMessage: GetDefaultMessageForStatusCode(Number);
        }


        public string GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400=>$" Bad Request ",
                401=>$" UnAuthorize",
                404=>$" Resource Not Found",
                500=>$"Error in this Path",
                _ => "No case availabe"
            };


        }
    }
}
