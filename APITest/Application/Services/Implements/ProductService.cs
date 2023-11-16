using APITest.Application.DTOs.Response;
using APITest.Application.DTOs.Response.Product;
using APITest.Application.Services.Interfaces;
using APITest.Application.Utilities;
using APITest.Domain.Interfaces;

namespace APITest.Application.Services.Implements
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IRepository repository, IReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository) { }

        public async Task<CRUDResult<IEnumerable<ProductRes>>> ReadAll()
        {
            try
            {
                var result = await ReadRepository.StoreProcedureQuery<ProductRes>("[dbo].[Product_ReadAll]", null);

                if (result == null || !result.Any())
                {
                    return Error<IEnumerable<ProductRes>>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: "Không có dữ liệu");
                }

                return Success(result);

            }
            catch (Exception ex)
            {
                return Error<IEnumerable<ProductRes>>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: ex.Message);
            }
        }
    }
}
