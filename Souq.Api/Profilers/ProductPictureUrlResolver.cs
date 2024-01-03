using AutoMapper;
using Souq.Api.DTOS;
using Souq.Core.DataBase;

namespace Souq.Api.Profilers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["ApiBaseurl"]}/{source.PictureUrl}";


            return  string.Empty;
        }
    }
}
