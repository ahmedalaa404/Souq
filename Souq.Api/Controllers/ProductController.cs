using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Core.Specification;

namespace Souq.Api.Controllers
{

    public class ProductController : ApiController // Container For Common Things Between All Controllers
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IMapper _Mapper;

        public ProductController(IGenericRepository<Product> ProductRepo ,IMapper Mapper)
        {
            _ProductRepo = ProductRepo;
            _Mapper = Mapper;
        }


        [HttpGet]
        //[ProducesDefaultResponseType(400)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts() 
        {
            //var Product = await _ProductRepo.GetAllAsyc();

            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products=await _ProductRepo.GetAllAsycWithSpec(Spec);

            var ProductDto= _Mapper.Map<IEnumerable<Product> ,IEnumerable<ProductToReturnDTO>>(Products);




            return Ok(ProductDto);
        }







        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {


            var Spec = new ProductWithBrandAndTypeSpecification( id);

            var Product =await _ProductRepo.GetByIdAsyncWithSpec(Spec);
            var ProductDto = _Mapper.Map<Product,ProductToReturnDTO>(Product);

            return Ok(ProductDto);
        }


    }
}
