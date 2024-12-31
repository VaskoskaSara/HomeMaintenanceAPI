using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class BookedDatesByEmployeeQueryHandler : IRequestHandler<BookedDatesByEmployee, List<DateTime>>
    {
        private readonly IEmployeeService _employeesService;
        public BookedDatesByEmployeeQueryHandler(IEmployeeService employeeService) {
            _employeesService = employeeService;
        }

        public async Task<List<DateTime>> Handle(BookedDatesByEmployee request, CancellationToken cancellationToken)
        {
            var response = await _employeesService.GetBookedDatesByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
