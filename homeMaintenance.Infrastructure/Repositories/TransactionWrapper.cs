using HomeMaintenanceApp.Web;
using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public class TransactionWrapper : ITransactionWrapper
    {
        private readonly IDbConnection _dbConnection;
        public TransactionWrapper(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
        }

        public async Task ExecuteAsync(Func<IDbTransaction, Task> operation)
        {
            IDbTransaction? dbTransaction = null;
            
            try{
                dbTransaction = _dbConnection.BeginTransaction();
                await operation(dbTransaction);
                dbTransaction.Commit();
            }
            catch
            {
                dbTransaction?.Rollback();
                throw;
            }
        }
    }
}
