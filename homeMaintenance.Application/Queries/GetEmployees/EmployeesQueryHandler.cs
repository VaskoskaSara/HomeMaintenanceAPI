using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetEmployees
{
    public class EmployeesQueryHandler : IRequestHandler<EmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(EmployeesQuery request, CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetEmployees(request.cities, request.price, request.experience, request.excludeByContract, request.categoryIds).ConfigureAwait(false);
            return response;
        }
    }
}
