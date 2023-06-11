using ChallengeAPI.Models.Entities;
using ChallengeAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Roles = "employee,manager")]
        public async Task<IActionResult> GetAll()
        {          
            return Ok(await _productService.GetAllAsync());
        }

        [HttpPost]
        [Authorize(Roles ="manager")]
        public async Task<IActionResult> SaveAsync([FromBody] Product product)
        {
            await _productService.SaveAsync(product);
            return Created("", product);
        }
    }
}
