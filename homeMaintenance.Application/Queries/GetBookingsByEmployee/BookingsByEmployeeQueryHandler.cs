using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class BookingsByEmployeeQueryHandler : IRequestHandler<BookingsByEmployeeQuery, IEnumerable<BookingInfo>>
    {
        private readonly IServiceContainer _serviceContainer;
        public BookingsByEmployeeQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<IEnumerable<BookingInfo>> Handle(BookingsByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetBookingsByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
