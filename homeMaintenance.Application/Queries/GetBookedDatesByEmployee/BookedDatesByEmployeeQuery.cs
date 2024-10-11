using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record BookedDatesByEmployee(Guid id) : IRequest<List<DateTime>>;
}
