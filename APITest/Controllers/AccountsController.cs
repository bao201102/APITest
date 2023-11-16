using APITest.Application.DTOs.Request.Account;
using APITest.Application.DTOs.Response.Product;
using APITest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Sign up account 
        /// </summary>
        /// 2023-11-16 - BaoNN
        /// <returns></returns>
        [HttpPost("sign-up")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> SignUp(AccountSignUpReq obj)
        {
            var products = await _service.SignUp(obj);
            return Ok(products);
        }
    }
}
