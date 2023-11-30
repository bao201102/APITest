﻿using APITest.Application.DTOs.Request;
using APITest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseController
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
            var result = await _service.SignUp(obj);
            return ApiOK(result);
        }

        /// <summary>
        /// Sign in account 
        /// </summary>
        /// 2023-11-17 - BaoNN
        /// <returns></returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> SignIn(AccountSignInReq obj)
        {
            var result = await _service.SignIn(obj);
            return ApiOK(result);
        }
    }
}
