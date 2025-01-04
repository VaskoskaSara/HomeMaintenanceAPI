using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetBookingsByUser
{
    public record BookingsByUserQuery(Guid id) : IRequest<IEnumerable<BookingInfoDto>>;
}
