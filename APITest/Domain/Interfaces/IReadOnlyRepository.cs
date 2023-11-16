using APITest.Domain.Entities;
using System.Data;

namespace APITest.Domain.Interfaces
{
    public interface IReadOnlyRepository : IDisposable
    {
        //IDbConnection Connection { get; }

        //IEnumerable<T> FindAll<T>() where T : class;

        //Task<IEnumerable<T>> FindAllAsync<T>() where T : class;

        //T GetById<T>(T obj) where T : class;

        //Task<T> GetByIdAsync<T>(T obj) where T : class;

        //IEnumerable<T> StoreProcedureQuery<T>(string StoreProcedureName, object param = null);

        //Task<IEnumerable<T>> ExcuteAsync<T>(string storeProcedureName, object param = null);

        //Task<SqlMapper.GridReader> ExecuteMultiAsync(string storeProcedureName, object param = null);

        //Task<SqlMapper.GridReader> QueryMultiAsync(string sqlQuery, object param = null);

        //IEnumerable<T> SQLQuery<T>(string query, object param = null);

        //Task<IEnumerable<T>> SQLQueryAsync<T>(string query, object param = null);

        //Task<T> SQLQueryFirstorDefaultAsync<T>(string query, object param = null);

        //IEnumerable<TReturn> SQLQuery<TFirst, TSecond, TReturn>(string SQLQuery, Func<TFirst, TSecond, TReturn> map, object param = null);

        //Task<IEnumerable<TReturn>> SQLQueryAsync<TFirst, TSecond, TReturn>(string SQLQuery, Func<TFirst, TSecond, TReturn> map, object param = null);

        //Task<bool> IsAvaiable();

        IDbConnection Connection { get; }

        IEnumerable<T> FindAll<T>() where T : BaseEntity;

        T GetById<T>(T obj) where T : BaseEntity;

        Task<IEnumerable<T>> StoreProcedureQuery<T>(string StoreProcedureName, object param = null);

        IEnumerable<T> SQLQuery<T>(string SQLQuery, object param = null);

        IEnumerable<TReturn> SQLQuery<TFirst, TSecond, TReturn>(string SQLQuery, Func<TFirst, TSecond, TReturn> map, object param = null);
    }
}
