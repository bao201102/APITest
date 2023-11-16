using APITest.Application.DTOs.Response.Product;
using APITest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// 2023-11-16 - BaoNN
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(ProductRes))]
        public async Task<IActionResult> ReadAll()
        {
            var products = await _service.ReadAll();
            return Ok(products);
        }
    }
}
