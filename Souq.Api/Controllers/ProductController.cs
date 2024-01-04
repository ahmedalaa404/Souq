using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
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
        [ProducesResponseType(typeof(IEnumerable<ProductToReturnDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
        {
            //var Product = await _ProductRepo.GetAllAsyc();

            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products = await _ProductRepo.GetAllAsycWithSpec(Spec);

            var ProductDto = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products);




            return Ok(ProductDto);
        }







        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {


            var Spec = new ProductWithBrandAndTypeSpecification( id);

            var Product =await _ProductRepo.GetByIdAsyncWithSpec(Spec);

            if (Product is null) 
                return NotFound(new ApiResponse(404,null));


            var ProductDto = _Mapper.Map<Product,ProductToReturnDTO>(Product);

            return Ok(ProductDto);
        }





        #region Test Exceptions Errors

        //[HttpGet("gETdSAA")]

        //public ActionResult GetData()
        //{
        //    string hambozo = null;
        //    var Spec = hambozo.ToString();

        //    return Ok();
        //} 
        //}
        #endregion


    }
}
