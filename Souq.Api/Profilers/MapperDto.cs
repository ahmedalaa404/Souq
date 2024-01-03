using AutoMapper;
using Souq.Api.DTOS;
using Souq.Core.DataBase;

namespace Souq.Api.Profilers
{
    public class MapperDto:Profile
    {
        public MapperDto()
        {
            CreateMap<Product, ProductToReturnDTO>().ForMember(distantion=> distantion.ProductBrand,o=>o.MapFrom(Source => Source.ProductBrand.Name))
           .ForMember(distantion => distantion.ProductType,o=>o.MapFrom(Source=> Source.ProductBrand.Name))
           .ForMember(Dis=>Dis.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>())
           ;
        }


    }
}
