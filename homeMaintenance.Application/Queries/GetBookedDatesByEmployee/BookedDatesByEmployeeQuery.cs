using MediatR;

namespace homeMaintenance.Application.Queries.GetBookedDatesByEmployee
{
    public record BookedDatesByEmployee(Guid id) : IRequest<List<DateTime>>;
}
