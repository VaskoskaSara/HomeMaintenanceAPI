using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record BookingsByEmployeeQuery(Guid id) : IRequest<IEnumerable<BookingInfo>>;
}
