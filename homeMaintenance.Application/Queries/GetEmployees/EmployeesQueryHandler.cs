using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class EmployeesQueryHandler : IRequestHandler<EmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IServiceContainer _serviceContainer;
        public EmployeesQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(EmployeesQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetEmployees(cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
