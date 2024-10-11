using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record DisabledDatesByEmployee(Guid id) : IRequest<List<DateOnly>>;
}
