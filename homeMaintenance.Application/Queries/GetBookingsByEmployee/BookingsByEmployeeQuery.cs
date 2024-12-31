using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record BookingsByEmployeeQuery(Guid id) : IRequest<IEnumerable<BookingInfoDto>>;
}
