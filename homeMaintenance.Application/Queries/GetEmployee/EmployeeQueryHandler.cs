using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class EmployeeQueryHandler : IRequestHandler<EmployeeQuery, UserDetailsDto>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeQueryHandler(IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        public async Task<UserDetailsDto> Handle(EmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetEmployeeById(request.id).ConfigureAwait(false);
            return response;
        }
    }
}
