using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public interface ITransactionWrapper
    {
        Task ExecuteAsync(Func<IDbTransaction, Task> operation);
    }
}
