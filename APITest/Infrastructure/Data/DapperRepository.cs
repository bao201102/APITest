using APITest.Domain.Interfaces;
using Dapper;
using System.Data;

namespace APITest.Infrastructure.Data
{
    public class DapperRepository : IRepository, IDisposable
    {
        private readonly IDbConnection _Connection;

        private bool disposedValue = false;

        public IDbConnection Connection => _Connection;

        public DapperRepository(IDbConnection Connection)
        {
            _Connection = Connection;
            if (_Connection.State == ConnectionState.Closed)
            {
                _Connection.Open();
            }
        }

        public async Task<int> ExecuteAsync(string StoreProcedureName, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null)
        {
            return await _Connection.ExecuteAsync(StoreProcedureName, param, transaction, commandTimeout, CommandType.StoredProcedure);
        }

        private void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            if (disposing && _Connection != null)
            {
                if (_Connection.State != 0)
                {
                    _Connection.Close();
                }

                _Connection.Dispose();
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~DapperRepository()
        {
            Dispose(disposing: false);
        }
    }
}
