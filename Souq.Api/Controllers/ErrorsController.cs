using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.Errors;

namespace Souq.Api.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi =true)]
        public ActionResult Errors(int Code)
        {
            return NotFound(new ApiResponse(Code, "U Access Path Or Action Not Found Must Check The Correct Path"));
        }

    }
}
