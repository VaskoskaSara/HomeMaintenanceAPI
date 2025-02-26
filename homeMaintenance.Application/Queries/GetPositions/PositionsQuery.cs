using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record PositionsQuery : IRequest<List<PositionDto>>;
}
