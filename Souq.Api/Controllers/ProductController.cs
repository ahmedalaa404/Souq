using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Core.DataBase;
using Souq.Core.Repositories;
using Souq.Core.Specification;

namespace Souq.Api.Controllers
{

    public class ProductController : ApiController // Container For Common Things Between All Controllers
    {
        private readonly IGenericRepository<Product> _ProductRepo;

        public ProductController(IGenericRepository<Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }


        [HttpGet]
        //[ProducesDefaultResponseType(400)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            //var Product = await _ProductRepo.GetAllAsyc();

            var Spec = new BaseSpecification<Product>();
            var Products=await _ProductRepo.GetAllAsycWithSpec(Spec);

            return Ok(Products);
        }







        [HttpPost("{Id}")]

        public async Task<ActionResult> GetProduct(int Id)
        {




            var Product =await _ProductRepo.GetByIdAsync(Id);
            return Ok(Product);
        }


    }
}
