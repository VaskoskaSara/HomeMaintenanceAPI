using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class BookingsByUserQueryHandler : IRequestHandler<BookingsByUserQuery, IEnumerable<BookingInfoDto>>
    {
        private readonly IUserService _userService;
        public BookingsByUserQueryHandler(IUserService userService) {
            _userService = userService;
        }

        public async Task<IEnumerable<BookingInfoDto>> Handle(BookingsByUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetBookingsByUser(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
