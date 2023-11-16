using APITest.Domain.Entities;
using APITest.Domain.Interfaces;
using Dapper;
using Dapper.FastCrud;
using System.Data;

namespace APITest.Infrastructure.Data
{
    public class DapperReadOnlyRepository : IReadOnlyRepository, IDisposable
    {
        private readonly IDbConnection _Connection;

        private bool disposedValue = false;

        public IDbConnection Connection => _Connection;

        public DapperReadOnlyRepository(IDbConnection Connection)
        {
            _Connection = Connection;
            if (_Connection.State == ConnectionState.Closed)
            {
                _Connection.Open();
            }
        }

        public IEnumerable<T> FindAll<T>() where T : BaseEntity
        {
            return _Connection.Find<T>();
        }

        public T GetById<T>(T obj) where T : BaseEntity
        {
            return _Connection.Get(obj);
        }

        public async Task<IEnumerable<T>> StoreProcedureQuery<T>(string StoreProcedureName, object param = null)
        {
            return await _Connection.QueryAsync<T>(StoreProcedureName, param, null, null, CommandType.StoredProcedure);
        }

        public IEnumerable<T> SQLQuery<T>(string SQLQuery, object param = null)
        {
            return _Connection.Query<T>(SQLQuery, param);
        }

        public IEnumerable<TReturn> SQLQuery<TFirst, TSecond, TReturn>(string SQLQuery, Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            return _Connection.Query(SQLQuery, map, param);
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

        ~DapperReadOnlyRepository()
        {
            Dispose(disposing: false);
        }
    }
}
