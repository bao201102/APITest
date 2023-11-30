using APITest.Application.DTOs.Request;
using APITest.Application.DTOs.Response;

namespace APITest.Application.Services.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(AccountSignInRes obj);
    }
}
