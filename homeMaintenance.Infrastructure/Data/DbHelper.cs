using Dapper;
using System.Data;

namespace homeMaintenance.Infrastructure.Data
{
    public class DbHelper : IDbHelper
    {
        private readonly IDbConnection _dbConnection;

        public DbHelper(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, object parameters = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<SqlMapper.GridReader> ExecuteQueryMultipleAsync(string sql, object parameters = null)
        {
            return await _dbConnection.QueryMultipleAsync(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            return await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            return await _dbConnection.ExecuteScalarAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task ExecuteStoredProcedureAsync(string storedProcedureName, object parameters = null)
        {
            await _dbConnection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        }


    }
}
