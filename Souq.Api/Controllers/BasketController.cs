using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Core.Entites;
using Souq.Core.Repositories;

namespace Souq.Api.Controllers
{

    public class BasketController : ApiController
    {
        private readonly IBasketRepo _BasketRepo;
        private readonly IMapper _Mapper;

        public BasketController(IBasketRepo Basket ,IMapper mapper)
        {
            _BasketRepo = Basket;
            _Mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string Id)
        {
            var Basket =await _BasketRepo.GetBasketAsync(Id);
            return Basket is null ? new CustomerBasket(Id) : Basket ;
        }


        [HttpPost]


        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO BasketDto)
        {
            var Basket = _Mapper.Map<CustomerBasketDTO, CustomerBasket>(BasketDto);
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
