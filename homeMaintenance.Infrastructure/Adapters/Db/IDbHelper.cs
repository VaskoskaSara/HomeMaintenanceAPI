using Dapper;

namespace homeMaintenance.Infrastructure.Adapters.Db
{
    public interface IDbHelper
    {
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null);
        Task ExecuteStoredProcedureAsync(string storedProcedureName, object parameters = null);
        Task<SqlMapper.GridReader> ExecuteQueryMultipleAsync(string sql, object parameters = null);
    }
}
