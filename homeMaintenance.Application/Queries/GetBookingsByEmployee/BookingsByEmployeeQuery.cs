using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetBookingsByEmployee
{
    public record BookingsByEmployeeQuery(Guid id) : IRequest<IEnumerable<BookingInfoDto>>;
}
