using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;

namespace APITest.Application.Utilities
{
    public static class QueryExtension
    {
        //    public static string ToJsonString<T>(this ICollection<T> data)
        //    {
        //        if (data == null || !data.Any())
        //        {
        //            return string.Empty;
        //        }

        //        IEnumerable<JObject> value = data.Select((T x) => JObject.FromObject(x).ToSnakeCase());
        //        return JsonConvert.SerializeObject(value);
        //    }

        //    public static string ToQueryString(this object obj)
        //    {
        //        IEnumerable<string> source = from p in obj.GetType().GetProperties()
        //                                     where p.GetValue(obj, null) != null
        //                                     select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)!.ToString());
        //        return "?" + string.Join("&", source.ToArray());
        //    }

        //    public static string ToQueryString(this IDictionary<string, object> dict)
        //    {
        //        List<string> list = new List<string>();
        //        foreach (KeyValuePair<string, object> item in dict)
        //        {
        //            list.Add(item.Key + "=" + item.Value);
        //        }

        //        return "?" + string.Join("&", list);
        //    }

        //    public static Dictionary<string, string> ToKeyPairs(this object obj)
        //    {
        //        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //        PropertyInfo[] properties = obj.GetType().GetProperties();
        //        foreach (PropertyInfo propertyInfo in properties)
        //        {
        //            dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(obj)!.ToString());
        //        }

        //        return dictionary;
        //    }

        //    public static DataTable ConvertJsonToDatatable(this string jsonString)
        //    {
        //        JArray jArray = JArray.Parse(jsonString);
        //        JArray jArray2 = new JArray();
        //        foreach (JObject item in jArray.Children<JObject>())
        //        {
        //            JObject jObject = new JObject();
        //            foreach (JProperty item2 in item.Properties())
        //            {
        //                if (item2.Value is JValue)
        //                {
        //                    jObject.Add(item2.Name, item2.Value);
        //                }
        //            }

        //            jArray2.Add(jObject);
        //        }

        //        return JsonConvert.DeserializeObject<DataTable>(jArray2.ToString());
        //    }

        //    public static string ToPostgresStoredStatement(this string storedName, DynamicParameters param, string[] resultParams)
        //    {
        //        string empty = string.Empty;
        //        if (param != null)
        //        {
        //            IEnumerable<string> values = param.ParameterNames.Select((string x) => "@" + x);
        //            empty = "(" + string.Join(",", values) + ")";
        //        }
        //        else
        //        {
        //            empty = "()";
        //        }

        //        string fetchQuery = string.Empty;
        //        if (resultParams?.Any() ?? false)
        //        {
        //            resultParams.ToList().ForEach(delegate (string x)
        //            {
        //                fetchQuery = fetchQuery + " FETCH ALL IN " + x + ";";
        //            });
        //        }

        //        return "CALL " + storedName + empty + "; " + fetchQuery;
        //    }

        //    public static void Reconnect(this IDbConnection connection)
        //    {
        //        //NpgsqlConnection npgsqlConnection = connection as NpgsqlConnection;
        //        //if (npgsqlConnection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        //        //{
        //        //    npgsqlConnection.Open();
        //        //}

        //        SqlConnection sqlConnection = connection as SqlConnection;
        //        if (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)
        //        {
        //            sqlConnection.Open();
        //        }
        //    }

        //    public static async Task<IEnumerable<T>> QueryStoredProcPgSql<T>(this IDbConnection connection, string procName, DynamicParameters parameters, string resultParam, IDbTransaction tran = null)
        //    {
        //        connection.Reconnect();
        //        IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
        //        try
        //        {
        //            string query = procName.ToPostgresStoredStatement(parameters, new string[1] { resultParam });
        //            SqlMapper.GridReader multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.Text, transaction: transaction, commandTimeout: 300);
        //            await multi.ReadAsync<object>();
        //            IEnumerable<T> result = await multi.ReadAsync<T>();
        //            if (tran == null)
        //            {
        //                transaction.Commit();
        //            }

        //            return result;
        //        }
        //        catch (NpgsqlException ex)
        //        {
        //            if (ex.Message.Contains("terminating connection due to administrator command"))
        //            {
        //                return await connection.QueryStoredProcPgSql<T>(procName, parameters, resultParam, tran);
        //            }

        //            transaction?.Rollback();
        //            throw ex;
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    public static async Task<T> QueryFirstStoredProcPgSql<T>(this IDbConnection connection, string procName, DynamicParameters parameters, string resultParam, IDbTransaction tran = null)
        //    {
        //        connection.Reconnect();
        //        IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
        //        try
        //        {
        //            string query = procName.ToPostgresStoredStatement(parameters, new string[1] { resultParam });
        //            SqlMapper.GridReader multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.Text, transaction: transaction, commandTimeout: 300);
        //            await multi.ReadAsync<object>();
        //            IEnumerable<T> result = await multi.ReadAsync<T>();
        //            if (tran == null)
        //            {
        //                transaction.Commit();
        //            }

        //            return result.FirstOrDefault();
        //        }
        //        catch (NpgsqlException ex)
        //        {
        //            if (ex.Message.Contains("terminating connection due to administrator command"))
        //            {
        //                return await connection.QueryFirstStoredProcPgSql<T>(procName, parameters, resultParam, tran);
        //            }

        //            transaction?.Rollback();
        //            throw ex;
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    public static async Task<int> ExecuteStoredProcPgSql(this IDbConnection connection, string procName, DynamicParameters parameters, string resultParam, IDbTransaction tran = null)
        //    {
        //        connection.Reconnect();
        //        IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
        //        try
        //        {
        //            parameters.Add(resultParam, 0);
        //            string query = procName.ToPostgresStoredStatement(parameters, null);
        //            CommandType? commandType = CommandType.Text;
        //            IEnumerable<int> result = await (await connection.QueryMultipleAsync(query, parameters, transaction, null, commandType)).ReadAsync<int>();
        //            if (tran == null)
        //            {
        //                transaction.Commit();
        //            }

        //            return result.First();
        //        }
        //        catch (NpgsqlException ex)
        //        {
        //            if (ex.Message.Contains("terminating connection due to administrator command"))
        //            {
        //                return await connection.ExecuteStoredProcPgSql(procName, parameters, resultParam, tran);
        //            }

        //            transaction?.Rollback();
        //            throw ex;
        //        }
        //        catch
        //        {
        //            transaction?.Rollback();
        //            throw;
        //        }
        //    }

        //    public static async Task<T> ExecuteStoredProcPgSql<T>(this IDbConnection connection, string procName, DynamicParameters parameters, string resultParam, IDbTransaction tran = null)
        //    {
        //        connection.Reconnect();
        //        IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
        //        try
        //        {
        //            parameters.Add(resultParam, default(T));
        //            string query = procName.ToPostgresStoredStatement(parameters, null);
        //            CommandType? commandType = CommandType.Text;
        //            IEnumerable<T> result = await (await connection.QueryMultipleAsync(query, parameters, transaction, null, commandType)).ReadAsync<T>();
        //            if (tran == null)
        //            {
        //                transaction.Commit();
        //            }

        //            return result.First();
        //        }
        //        catch (NpgsqlException ex)
        //        {
        //            if (ex.Message.Contains("terminating connection due to administrator command"))
        //            {
        //                return await connection.ExecuteStoredProcPgSql<T>(procName, parameters, resultParam, tran);
        //            }

        //            transaction?.Rollback();
        //            throw ex;
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    public static async Task<SqlMapper.GridReader> QueryMultiStoredProcPgSql(this IDbConnection connection, string procName, DynamicParameters parameters, params string[] resultParams)
        //    {
        //        connection.Reconnect();
        //        try
        //        {
        //            string query = procName.ToPostgresStoredStatement(parameters, resultParams);
        //            CommandType? commandType = CommandType.Text;
        //            SqlMapper.GridReader result = await connection.QueryMultipleAsync(query, parameters, null, null, commandType);
        //            await result.ReadAsync<object>();
        //            return result;
        //        }
        //        catch (NpgsqlException ex)
        //        {
        //            if (ex.Message.Contains("terminating connection due to administrator command"))
        //            {
        //                return await connection.QueryMultiStoredProcPgSql(procName, parameters, resultParams);
        //            }

        //            throw ex;
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //    }
    }
}
