using APITest.Application.DTOs.Request.Account;
using APITest.Application.DTOs.Response;
using APITest.Application.Helpers;
using APITest.Application.Services.Interfaces;
using APITest.Application.Utilities;
using APITest.Domain.Interfaces;
using Dapper;
using System.Data;

namespace APITest.Application.Services.Implements
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IRepository repository, IReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository) { }

        public async Task<CRUDResult<bool>> SignUp(AccountSignUpReq obj)
        {
            using (var tran = Repository.Connection.BeginTransaction())
            {
                try
                {
                    var passwordHelper = new PasswordHelper();
                    string hashedPassword = passwordHelper.HashPassword(obj.password);

                    // Kiểm tra mật khẩu khi đăng nhập
                    //bool isPasswordCorrect = passwordHelper.VerifyPassword("user_password", hashedPassword);

                    var parameters = new DynamicParameters();
                    parameters.Add("@email", obj.email);
                    parameters.Add("@password", hashedPassword);
                    parameters.Add("@name", obj.name);

                    var result = await Repository.Connection.ExecuteAsync("[dbo].[Account_SignUp]", parameters, tran, commandType: CommandType.StoredProcedure);

                    if (result < 1)
                    {
                        tran.Rollback();
                        return Error(statusCode: CRUDStatusCodeRes.InvalidData, msgError: "Dữ liệu chưa được cập nhật", data: false);
                    }

                    tran.Commit();
                    return Success(true);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return Error(statusCode: CRUDStatusCodeRes.InvalidData, msgError: ex.Message, data: false);
                }
            }
        }
    }
}
