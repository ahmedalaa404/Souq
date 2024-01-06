using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.Errors;
using Souq.Core.Entites;
using Souq.Core.Repositories;

namespace Souq.Api.Controllers
{

    public class BasketController : ApiController
    {
        private readonly IBasketRepo _BasketRepo;

        public BasketController(IBasketRepo Basket)
        {
            _BasketRepo = Basket;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string Id)
        {
            var Basket =await _BasketRepo.GetBasketAsync(Id);
            return Basket is null ? new CustomerBasket(Id) : Basket ;
        }


        [HttpPost]


        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var CreateOrUpdate=await _BasketRepo.UpdateBasketAsync(Basket);
            if (CreateOrUpdate is null)
                return BadRequest(new ApiResponse(400));
            return Ok(CreateOrUpdate);

        }

        [HttpDelete]


        public async Task<ActionResult<bool>> DeleteBasket (string Id)
        {
            return await _BasketRepo.DeleteBasketAsync(Id);
        }


    }
}
