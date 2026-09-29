using System.Collections.Generic;

namespace eShop.DataStore.SQL.Dapper
{
    public interface ISqlDataAccess
    {
        IEnumerable<T> LoadData<T, U>(string sql, U parameters);
        T LoadSingleData<T, U>(string sql, U parameters);
        void ExecuteCommand<T>(string sql, T parameters);
        IEnumerable<T> Query<T, U>(string sql, U parameters);
        T QuerySingle<T, U>(string sql, U parameters);
        T ExecuteScalar<T, U>(string sql, U parameters);
    }
}
