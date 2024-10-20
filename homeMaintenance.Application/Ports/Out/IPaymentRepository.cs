using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.Out
{
    public interface IPaymentRepository
    {
        Task<bool> SaveTransactionAsync(TransactionInfo transaction);
    }
}
