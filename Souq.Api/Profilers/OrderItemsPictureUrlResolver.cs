using AutoMapper;
using Souq.Api.DTOS;
using Souq.Core.DataBase;
using Souq.Core.Entites.Order_Aggregate;

namespace Souq.Api.Profilers
{
    public class OrderItemsPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemsPictureUrlResolver(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Product.ProductPictureUrl))
            {
                return $"{configuration["ApiBaseurl"]}/{source.Product.ProductPictureUrl}";
            }
            return null;
        }
    }
}
