using APITest.Application.DTOs.Request.Product;
using APITest.Application.DTOs.Response;
using APITest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
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
            var result = await _service.ReadAll();
            return ApiOK(result);
        }

        /// <summary>
        /// Search Product
        /// </summary>
        /// 2023-11-17 - BaoNN
        /// <returns></returns>
        [HttpPost("search")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(PagingResponse<ProductSearchRes>))]
        public async Task<IActionResult> Search(ProductSearchReq obj)
        {
            var result = await _service.Search(obj);
            return ApiOK(result);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// 2023-11-17 - BaoNN
        /// <returns></returns>
        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> Create(ProductCreateReq obj)
        {
            var result = await _service.Create(obj, CurrentUser.UserID);
            return ApiOK(result);
        }
    }
}
