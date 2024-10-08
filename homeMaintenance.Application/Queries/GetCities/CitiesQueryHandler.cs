using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class CitiesQueryHandler : IRequestHandler<CitiesQuery, IEnumerable<string>>
    {
        private readonly IServiceContainer _serviceContainer;
        public CitiesQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<IEnumerable<string>> Handle(CitiesQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetCities(cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
