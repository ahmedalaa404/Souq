using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Core.Entites;
using Souq.Core.Services;

namespace Souq.Api.Controllers
{

    public class PaymentController : ApiController
    {
        private readonly IPaymentServices paymentservices;
        private readonly IMapper mapper;

        public PaymentController(IPaymentServices paymentservices,IMapper mapper)
        {
            this.paymentservices = paymentservices;
            this.mapper = mapper;
        }



        [HttpPost("id")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string id) //BasketId
        {
            var Basket=await paymentservices.CreateOrUpdatePaymentIntent(id);
            if (Basket is null)
                return BadRequest(new ApiResponse(400, "problem from U Basket"));
            return Ok(mapper.Map<CustomerBasketDTO>(Basket));
        }
    }
}
