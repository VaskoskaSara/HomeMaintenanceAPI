using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetBookingsByEmployee
{
    public class BookingsByEmployeeQueryHandler : IRequestHandler<BookingsByEmployeeQuery, IEnumerable<BookingInfoDto>>
    {
        private readonly IEmployeeService _employeeService;
        public BookingsByEmployeeQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<BookingInfoDto>> Handle(BookingsByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetBookingsByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
