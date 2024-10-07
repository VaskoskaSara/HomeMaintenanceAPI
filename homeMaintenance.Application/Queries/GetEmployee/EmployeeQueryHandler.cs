using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class EmployeeQueryHandler : IRequestHandler<EmployeeQuery, UserDetails>
    {
        private readonly IServiceContainer _serviceContainer;
        public EmployeeQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<UserDetails> Handle(EmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetEmployeeById(request.id).ConfigureAwait(false);
            return response;
        }
    }
}
