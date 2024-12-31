using Dapper;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbConnection _dbConnection;

        public PaymentRepository(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
        }
        public async Task<bool> SaveTransactionAsync(TransactionInfo transaction)
        {
            var response = await _dbConnection.ExecuteAsync("InsertTransaction",
            new
            {
                  userId = transaction.UserId,
                  employeeId = transaction.EmployeeId,
                  amount = transaction.Amount,
                  paymentId = transaction.PaymentId,
                  startDateTime = transaction.StartDateTime,
                  endDateTime = transaction.EndDateTime,
            },
            commandType: CommandType.StoredProcedure);

            return response == -1;
        }
    }
}
