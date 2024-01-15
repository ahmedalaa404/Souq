using AutoMapper;
using Souq.Api.DTOS;
using Souq.Core.DataBase;
using Souq.Core.Entites;
using Identity= Souq.Core.Entites.Identity;
using Aggregate=Souq.Core.Entites.Order_Aggregate;
using Souq.Core.Entites.Order_Aggregate;

namespace Souq.Api.Profilers
{
    public class MapperDto:Profile
    {
        public MapperDto()
        {
            CreateMap<Product, ProductToReturnDTO>().ForMember(distantion => distantion.ProductBrand, o => o.MapFrom(Source => Source.ProductBrand.Name))
           .ForMember(distantion => distantion.ProductType, o => o.MapFrom(Source => Source.ProductBrand.Name))
           .ForMember(Dis => Dis.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<AddressDto, Aggregate.Address>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>().ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(x=>x.DeliveryMethod.ShortName))
                .ForMember(d=>d.Coast,o=>o.MapFrom(s=>s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>().ForMember(x => x.ProductId, o => o.MapFrom(x => x.Product.ProductId))
            .ForMember(x => x.ProductName, o => o.MapFrom(x => x.Product.ProductName))
            .ForMember(x => x.ProductPictureUrl, o => o.MapFrom(x => x.Product.ProductPictureUrl))
            .ForMember(x => x.ProductPictureUrl, o => o.MapFrom<OrderItemsPictureUrlResolver>());

        }


    }
}
