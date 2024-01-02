using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Souq.Core.DataBase;
using Souq.Core.Repositories;

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
        public async Task<IActionResult> GetProducts() 
        {
            var Product = await _ProductRepo.GetAllAsyc();


            return Ok(Product);
        }

    }
}
