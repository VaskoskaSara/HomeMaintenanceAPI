using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record PositionsQuery : IRequest<IEnumerable<Position>>;
}
