using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class DisabledDatesByEmployeeQueryHandler : IRequestHandler<DisabledDatesByEmployee, List<DateOnly>>
    {
        private readonly IEmployeeService _employeeService;

        public DisabledDatesByEmployeeQueryHandler(IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        public async Task<List<DateOnly>> Handle(DisabledDatesByEmployee request, CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetDisabledDatesByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
