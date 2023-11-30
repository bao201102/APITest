using APITest.Application.DTOs.Request.Product;
using APITest.Application.DTOs.Response;

namespace APITest.Application.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<CRUDResult<IEnumerable<ProductRes>>> ReadAll();
        Task<PagingResponse<ProductSearchRes>> Search(ProductSearchReq obj);
        Task<CRUDResult<bool>> Create(ProductCreateReq obj, int userID);
    }
}
