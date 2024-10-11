using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class DisabledDatesByEmployeeQueryHandler : IRequestHandler<DisabledDatesByEmployee, List<DateOnly>>
    {
        private readonly IServiceContainer _serviceContainer;
        public DisabledDatesByEmployeeQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<List<DateOnly>> Handle(DisabledDatesByEmployee request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetDisabledDatesByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
