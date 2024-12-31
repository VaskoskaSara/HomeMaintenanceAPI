using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public class TransactionInfoCommandHandler : IRequestHandler<TransactionInfoCommand, bool>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public TransactionInfoCommandHandler(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(TransactionInfoCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<TransactionInfo>(request);
            var result = await _paymentService.SaveTransactionInfo(transaction, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
