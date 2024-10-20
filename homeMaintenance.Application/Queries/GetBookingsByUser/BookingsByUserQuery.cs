using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record BookingsByUserQuery(Guid id) : IRequest<IEnumerable<BookingInfo>>;
}
