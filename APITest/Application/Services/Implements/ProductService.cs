using APITest.Application.Configs;
using APITest.Application.DTOs.Request.Product;
using APITest.Application.DTOs.Response;
using APITest.Application.Services.Interfaces;
using APITest.Application.Utilities;
using APITest.Domain.Interfaces;
using Dapper;

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
                var result = await ReadRepository.QueryStoredProc<ProductRes>("[dbo].[Product_ReadAll]", null);

                if (result == null || !result.Any())
                {
                    return Error<IEnumerable<ProductRes>>(statusCode: CRUDStatusCodeRes.ResourceNotFound);
                }

                return Success(result);

            }
            catch (Exception ex)
            {
                return Error<IEnumerable<ProductRes>>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: ex.GetExceptionMessage());
            }
        }

        public async Task<PagingResponse<ProductSearchRes>> Search(ProductSearchReq obj)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@keysearch", obj.keysearch);
                parameters.Add("@date_from", obj.date_from);
                parameters.Add("@date_to", obj.date_to);
                parameters.Add("@page_size", obj.page_size);
                parameters.Add("@page_index", obj.page_index);

                var result = await ReadRepository.QueryStoredProc<ProductSearchRes>("[dbo].[Product_Search]", parameters);

                if (result == null || !result.Any())
                {
                    return PagingError<ProductSearchRes>(statusCode: CRUDStatusCodeRes.ResourceNotFound);
                }

                var total_record = result.FirstOrDefault().total_record;

                return PagingSuccess(result, obj.page_index, obj.page_size, total_record);

            }
            catch (Exception ex)
            {
                return PagingError<ProductSearchRes>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: ex.GetExceptionMessage());
            }
        }

        public async Task<CRUDResult<bool>> Create(ProductCreateReq obj, int userID)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", obj.name);
                parameters.Add("@img_url", obj.img_url);
                parameters.Add("@quantity", obj.quantity);
                parameters.Add("@unit_price", obj.unit_price);
                parameters.Add("@user_id", userID);

                var result = await Repository.ExecuteAsync("[dbo].[Product_Create]", parameters);

                if (result < 1)
                {
                    return Error<bool>(statusCode: CRUDStatusCodeRes.InvalidData, msgError: "Lỗi cập nhật dữ liệu");
                }

                return Success(true);
            }
            catch (Exception ex)
            {
                return Error<bool>(statusCode: CRUDStatusCodeRes.ResetContent, msgError: ex.GetExceptionMessage());
            }
        }
    }
}
