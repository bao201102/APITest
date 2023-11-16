using APITest.Domain.Entities;
using APITest.Domain.Interfaces;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
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

        //public T Insert<T>(T entity, IDbTransaction transaction = null) where T : BaseEntity
        //{
        //    if (transaction == null)
        //    {
        //        _Connection.Insert(entity);
        //    }
        //    else
        //    {
        //        _Connection.Insert(entity, delegate (IStandardSqlStatementOptionsBuilder<T> s)
        //        {
        //            s.AttachToTransaction(transaction);
        //        });
        //    }

        //    return entity;
        //}

        //public IEnumerable<T> Insert<T>(IEnumerable<T> entities, IDbTransaction transaction = null) where T : BaseEntity
        //{
        //    if (transaction == null)
        //    {
        //        foreach (T entity in entities)
        //        {
        //            _Connection.Insert(entity);
        //        }
        //    }
        //    else
        //    {
        //        foreach (T entity2 in entities)
        //        {
        //            _Connection.Insert(entity2, delegate (IStandardSqlStatementOptionsBuilder<T> s)
        //            {
        //                s.AttachToTransaction(transaction);
        //            });
        //        }
        //    }

        //    return entities;
        //}

        //public void ExcuteStore(string storeName, object param = null, IDbTransaction transaction = null)
        //{
        //}

        //public T Update<T>(T entity, IDbTransaction transaction = null) where T : BaseEntity
        //{
        //    if (transaction == null)
        //    {
        //        _Connection.Update(entity);
        //    }
        //    else
        //    {
        //        _Connection.Update(entity, delegate (IStandardSqlStatementOptionsBuilder<T> s)
        //        {
        //            s.AttachToTransaction(transaction);
        //        });
        //    }

        //    return entity;
        //}

        //public IEnumerable<T> Update<T>(IEnumerable<T> entities, IDbTransaction transaction = null) where T : BaseEntity
        //{
        //    if (transaction == null)
        //    {
        //        foreach (T entity in entities)
        //        {
        //            _Connection.Update(entity);
        //        }
        //    }
        //    else
        //    {
        //        foreach (T entity2 in entities)
        //        {
        //            _Connection.Update(entity2, delegate (IStandardSqlStatementOptionsBuilder<T> s)
        //            {
        //                s.AttachToTransaction(transaction);
        //            });
        //        }
        //    }

        //    return entities;
        //}

        //public int BulkUpdate<T>(T entity, Action<IConditionalBulkSqlStatementOptionsBuilder<T>> statementOptions = null) where T : BaseEntity
        //{
        //    if (statementOptions == null)
        //    {
        //        return _Connection.BulkUpdate(entity);
        //    }

        //    return _Connection.BulkUpdate(entity, statementOptions);
        //}

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
