using APITest.Application.DTOs.Request.Account;
using APITest.Application.DTOs.Response;

namespace APITest.Application.Services.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<CRUDResult<bool>> SignUp(AccountSignUpReq obj);
    }
}
