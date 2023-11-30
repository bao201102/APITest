using APITest.Application.Configs;
using APITest.Application.DTOs.Response;
using APITest.Domain.Interfaces;

namespace APITest.Application.Utilities
{
    public abstract class BaseService : IDisposable
    {
        private readonly IRepository _repository;
        protected IRepository Repository => _repository;

        private readonly IReadOnlyRepository _readOnlyRepository;
        protected IReadOnlyRepository ReadRepository => _readOnlyRepository;

        #region Constructor
        public BaseService() { }

        public BaseService(IRepository repository, IReadOnlyRepository readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
        }

        #endregion

        #region Function
        protected CRUDResult<T> Success<T>(T data, string message = null)
        {
            var result = new CRUDResult<T>()
            {
                Data = data,
                StatusCode = CRUDStatusCodeRes.Success,
                ErrorMessage = message
            };

            return result;
        }

        protected CRUDResult<T> Error<T>(T data = default(T), CRUDStatusCodeRes statusCode = CRUDStatusCodeRes.InvalidData, string msgError = "")
        {
            var result = new CRUDResult<T>()
            {
                Data = data,
                StatusCode = statusCode,
                ErrorMessage = string.IsNullOrEmpty(msgError) ? Constants.ErrorCodes[(int)ErrorCodeEnum.NotFound] : msgError.Replace("ERROR:", "")
            };

            return result;
        }

        protected PagingResponse<T> PagingSuccess<T>(IEnumerable<T> data, int pageIndex, int pageSize, int totalRecord = 0)
        {
            var response = new PagingResponse<T>
            {
                CurrentPageIndex = pageIndex,
                PageSize = pageSize,
                StatusCode = CRUDStatusCodeRes.Success,
                ErrorMessage = string.Empty
            };

            // Handle paging from DB
            if (totalRecord > 0 || pageSize == -1 || pageIndex == -1)
            {
                response.Records = data.ToList();
                response.TotalRecord = totalRecord;
            }
            else
            {
                response.Records = data.Skip(pageSize * pageIndex).Take(pageSize).ToList();
                response.TotalRecord = data.Count();
            }

            return response;
        }

        protected PagingResponse<T> PagingError<T>(T data = default(T), CRUDStatusCodeRes statusCode = CRUDStatusCodeRes.InvalidData, string msgError = "", int pageIndex = 0, int pageSize = 10)
        {
            return new PagingResponse<T>
            {
                StatusCode = statusCode,
                ErrorMessage = msgError,
                CurrentPageIndex = pageIndex,
                PageSize = pageSize
            };
        }
        #endregion

        #region Dispose
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_repository != null)
                    {
                        _repository.Dispose();
                    }

                    if (_readOnlyRepository != null)
                    {
                        _readOnlyRepository.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }
        #endregion
    }
}
