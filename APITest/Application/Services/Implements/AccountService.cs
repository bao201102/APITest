using APITest.Application.Configs;
using APITest.Application.DTOs.Request;
using APITest.Application.DTOs.Response;
using APITest.Application.Helpers;
using APITest.Application.Services.Interfaces;
using APITest.Application.Utilities;
using APITest.Domain.Interfaces;
using Dapper;

namespace APITest.Application.Services.Implements
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IJwtProvider _jwtProvider;

        public AccountService(IRepository repository, IReadOnlyRepository readOnlyRepository, IJwtProvider jwtProvider)
            : base(repository, readOnlyRepository)
        {
            _jwtProvider = jwtProvider;
        }

        public async Task<CRUDResult<bool>> SignUp(AccountSignUpReq obj)
        {
            using (var tran = Repository.Connection.BeginTransaction())
            {
                try
                {
                    var passwordHelper = new PasswordHelper();
                    string hashedPassword = passwordHelper.HashPassword(obj.password);

                    var parameters = new DynamicParameters();
                    parameters.Add("@email", obj.email);
                    parameters.Add("@password", hashedPassword);
                    parameters.Add("@name", obj.name);

                    var result = await Repository.ExecuteAsync("[dbo].[Account_SignUp]", parameters, tran);

                    if (result < 1)
                    {
                        tran.Rollback();
                        return Error<bool>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: "Dữ liệu chưa được cập nhật");
                    }

                    tran.Commit();
                    return Success(true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return Error<bool>(statusCode: CRUDStatusCodeRes.ResetContent, msgError: ex.GetExceptionMessage());
                }
            }
        }

        public async Task<CRUDResult<string>> SignIn(AccountSignInReq obj)
        {
            try
            {
                var passwordHelper = new PasswordHelper();
                string hashedPassword = passwordHelper.HashPassword(obj.password);

                var parameters = new DynamicParameters();
                parameters.Add("@email", obj.email);
                parameters.Add("@password", hashedPassword);

                var result = await ReadRepository.QueryFirstStoredProc<AccountSignInRes>("[dbo].[Account_SignIn]", parameters);

                if (result == null)
                {
                    return Error<string>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: "Email hoặc mật khẩu không đúng");
                }

                var token = _jwtProvider.GenerateToken(result);

                return Success(token);
            }
            catch (Exception ex)
            {
                return Error<string>(statusCode: CRUDStatusCodeRes.ResetContent, msgError: ex.GetExceptionMessage());
            }
        }
    }
}
