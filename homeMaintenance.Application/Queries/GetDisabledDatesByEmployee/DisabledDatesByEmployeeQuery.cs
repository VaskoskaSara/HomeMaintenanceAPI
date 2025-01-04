using MediatR;

namespace homeMaintenance.Application.Queries.GetDisabledDatesByEmployee
{
    public record DisabledDatesByEmployee(Guid id) : IRequest<List<DateOnly>>;
}
