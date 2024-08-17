using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record CitiesQuery : IRequest<IEnumerable<string>>;
}
