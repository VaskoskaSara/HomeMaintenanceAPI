using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public class TransactionInfoCommandHandler : IRequestHandler<TransactionInfoCommand, bool>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public TransactionInfoCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<bool> Handle(TransactionInfoCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<TransactionInfo>(request);
            var result = await _serviceContainer.PaymentService.SaveTransactionInfo(transaction, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
