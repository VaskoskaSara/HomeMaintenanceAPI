using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Adapters.Db;

namespace homeMaintenance.Infrastructure.Adapters.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbHelper _dbHelper;

        public PaymentRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<bool> SaveTransactionAsync(TransactionInfo transaction)
        {
            var response = await _dbHelper.ExecuteAsync("InsertTransaction",
            new
            {
                userId = transaction.UserId,
                employeeId = transaction.EmployeeId,
                amount = transaction.Amount,
                paymentId = transaction.PaymentId,
                startDateTime = transaction.StartDateTime,
                endDateTime = transaction.EndDateTime,
            });

            return response == -1;
        }
    }
}
