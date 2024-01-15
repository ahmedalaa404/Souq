using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Repositories;
using Souq.Core.Services;

using System.Security.Claims;


namespace Souq.Api.Controllers
{
    
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _Mapper;
        private readonly IUniteOFWork _UniteOFWork;

        public OrderController(IOrderServices OrderServices , IMapper Map , IUniteOFWork UniteOFWork)
        {
            _orderServices = OrderServices;
            _Mapper = Map;
            _UniteOFWork = UniteOFWork;
        }

        [Authorize]
        [HttpPost]//Post : /api/Orders
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto  orderDto  /* From Query String*/ )
        {

            var BuyerEmailUser=User.FindFirstValue(ClaimTypes.Email);

            var ShippingAddress = _Mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var Order= await _orderServices.CreateOrderAsync(BuyerEmailUser, orderDto.BasketId, orderDto.DeliveryMethodId, ShippingAddress);
            // basketId   // Shipping Address   // DeliveryMethod
            if (Order is null)
                return BadRequest(new ApiResponse(400));
            var Dto = _Mapper.Map<Order, OrderToReturnDto>(Order);
            return Ok(Dto);   

        }

        [HttpGet]

        [Authorize]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderServices.GetOrdersForUserAsync(BuyerEmail);
            var Dto = _Mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(Dto);
        
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int Id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderServices.GetOrderByIdForUserAsync(Id, Email);
            if (Order is null)
                return BadRequest(new ApiResponse(400));
            else
            {
                var Dto = _Mapper.Map<Order, OrderToReturnDto>(Order);
                return Ok(Dto);
            }


        }


        [HttpGet("DeliveryMethods")] // GET: /API/ORDERS

        [ProducesResponseType(typeof(DeliveryMethod), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {

            var DeliveryMethod= await _UniteOFWork.Repositorey<DeliveryMethod>().GetAllAsyc();

            if(DeliveryMethod is not null)
                return Ok(DeliveryMethod);
            return BadRequest(new ApiResponse(404));
        }





    }
}
