using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Core.Entites;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Services;
using Stripe;

namespace Souq.Api.Controllers
{

    public class PaymentController : ApiController
    {
        private readonly IPaymentServices paymentservices;
        private readonly IMapper mapper;
        private readonly ILogger loger;

        public PaymentController(IPaymentServices paymentservices,IMapper mapper,ILogger loger)
        {
            this.paymentservices = paymentservices;
            this.mapper = mapper;
            this.loger = loger;
        }



        [HttpPost("id")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string id) //BasketId
        {
            var Basket=await paymentservices.CreateOrUpdatePaymentIntent(id);
            if (Basket is null)
                return BadRequest(new ApiResponse(400, "problem from U Basket"));
            return Ok(mapper.Map<CustomerBasketDTO>(Basket));
        }
      private  const string endpointSecret = "whsec_3736f0f00dea1af1b17ce960f98d8f9af6c745f91e1da57118d1543f89bc68bd";


        [HttpPost("webhook")]

        public async Task<IActionResult> StriptWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);
                var Paymentintent =(PaymentIntent) stripeEvent.Data.Object;
                Order order;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {

                    order = await paymentservices.UpdatePaymentIntentToSucceededOrFailed(Paymentintent.Id, false);
                    loger.LogInformation("Payment is Successed Ya Hambozoo", Paymentintent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    order= await paymentservices.UpdatePaymentIntentToSucceededOrFailed(Paymentintent.Id, true);
                    loger.LogInformation("Payment is Successed Ya Hambozoo", Paymentintent.Id);

                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
     
        }
    }
}
