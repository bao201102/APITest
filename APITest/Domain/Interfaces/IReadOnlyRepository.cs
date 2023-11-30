using Dapper;
using System.Data;

namespace APITest.Domain.Interfaces
{
    public interface IReadOnlyRepository : IDisposable
    {
        IDbConnection Connection { get; }

        /// <summary>
        /// Used to get list data using Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        Task<IEnumerable<T>> QueryStoredProc<T>(string StoreProcedureName, DynamicParameters param, IDbTransaction? transaction = null, int? commandTimeout = null);

        /// <summary>
        /// Used to get single-row data using Stored Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        Task<T> QueryFirstStoredProc<T>(string StoreProcedureName, DynamicParameters param, IDbTransaction? transaction = null, int? commandTimeout = null);

        #region PostgreSql
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
        #endregion
    }
}
