using APITest.Application.DTOs.Response;
using APITest.Application.DTOs.Response.Product;

namespace APITest.Application.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<CRUDResult<IEnumerable<ProductRes>>> ReadAll();
    }
}
