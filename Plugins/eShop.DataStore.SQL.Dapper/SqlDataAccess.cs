using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace eShop.DataStore.SQL.Dapper
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly string connectionString;

        public SqlDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<T> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql, parameters);
            }
        }

        public T LoadSingleData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<T>(sql, parameters);
            }
        }

        public void ExecuteCommand<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

        public IEnumerable<T> Query<T, U>(string sql, U parameters)
        {
            return LoadData<T, U>(sql, parameters);
        }

        public T QuerySingle<T, U>(string sql, U parameters)
        {
            return LoadSingleData<T, U>(sql, parameters);
        }

        public T ExecuteScalar<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.ExecuteScalar<T>(sql, parameters);
            }
        }
    }
}
