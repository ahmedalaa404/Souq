using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS;
using Souq.Api.Errors;
using Souq.Api.Helper;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Core.Specification;

namespace Souq.Api.Controllers
{

    public class ProductController : ApiController // Container For Common Things Between All Controllers
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IMapper _Mapper;
        private readonly IGenericRepository<ProductBrand> _Productbrand;
        private readonly IGenericRepository<ProductType> _ProductType;

        public ProductController(IGenericRepository<Product> ProductRepo ,IMapper Mapper, 
            IGenericRepository<ProductBrand> Productbrand, IGenericRepository<ProductType> ProductType)
        {
            _ProductRepo = ProductRepo;
            _Mapper = Mapper;
            _Productbrand = Productbrand;
            _ProductType= ProductType;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PaginationDataDto<ProductToReturnDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginationDataDto<ProductToReturnDTO>>> GetProducts([FromQuery] ProductWithParam param)
        {
            //var Product = await _ProductRepo.GetAllAsyc();

            var Spec = new ProductWithBrandAndTypeSpecification(param);


            var Products = await _ProductRepo.GetAllAsycWithSpec(Spec);

            var ProductDto = _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);

            var specCount = new GetCountFromProductWithspec(param);
            var Count = await _ProductRepo.GetAllAsycWithSpec(specCount); 

            return Ok(new PaginationDataDto<ProductToReturnDTO>(param.PageIndex, param.PageSize, Count.Count(), ProductDto));
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



        #region Brand And Type Action 
        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetType()
        {
            var Type = await _ProductType.GetAllAsyc();
            return Ok(Type);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var Type = await  _Productbrand.GetAllAsyc();
            return Ok(Type);
        }

        #endregion










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
