using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> SaveTransactionInfo(TransactionInfo transaction, CancellationToken cancellationToken = default)
        {
            if (transaction.UserId.Equals(transaction.EmployeeId))
            {
                throw new InvalidOperationException();
            }

            var response = await _paymentRepository.SaveTransactionAsync(transaction);

            return response;
        }
    }
}
