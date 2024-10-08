using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.In
{
    public interface IPaymentService
    {
        Task<bool> SaveTransactionInfo(TransactionInfo transaction, CancellationToken cancellationToken = default);
    }
}
