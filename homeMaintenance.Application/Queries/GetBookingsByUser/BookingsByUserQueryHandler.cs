using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class BookingsByUserQueryHandler : IRequestHandler<BookingsByUserQuery, IEnumerable<BookingInfo>>
    {
        private readonly IServiceContainer _serviceContainer;
        public BookingsByUserQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<IEnumerable<BookingInfo>> Handle(BookingsByUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetBookingsByUser(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
