using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record BookingsByUserQuery(Guid id) : IRequest<IEnumerable<BookingInfoDto>>;
}
