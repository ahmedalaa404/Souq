using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Services;
using System.Security.Claims;


namespace Souq.Api.Controllers
{
    
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _Mapper;

        public OrderController(IOrderServices OrderServices , IMapper Map)
        {
            _orderServices = OrderServices;
            _Mapper = Map;
        }

        [Authorize]
        [HttpPost]//Post : /api/Orders
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<Order>> CreateOrder(OrderDto  orderDto  /* From Query String*/ )
        {

            var BuyerEmailUser=User.FindFirstValue(ClaimTypes.Email);

            var ShippingAddress = _Mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var Order= await _orderServices.CreateOrderAsync(BuyerEmailUser, orderDto.BasketId, orderDto.DeliveryMethodId, ShippingAddress);
            // basketId   // Shipping Address   // DeliveryMethod
            if (Order is null)
                return BadRequest(new ApiResponse(400));
            return Ok(Order);   

        }


    }
}
