using APITest.Application.Services.Interfaces;
using APITest.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace APITest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtProvider _jwtProvider;

        public AuthController(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public IActionResult Login(string username)
        {
            // Perform authentication logic (e.g., check username and password)

            // If authentication is successful, generate a token
            var token = _jwtProvider.GenerateToken(username);

            // Return the token to the client
            return Ok(token);
        }
    }
}
