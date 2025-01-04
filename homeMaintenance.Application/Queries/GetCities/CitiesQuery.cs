using MediatR;

namespace homeMaintenance.Application.Queries.GetCities
{
    public record CitiesQuery : IRequest<IEnumerable<string>>;
}
