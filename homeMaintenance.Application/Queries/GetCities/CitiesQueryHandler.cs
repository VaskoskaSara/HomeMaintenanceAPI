using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetCities
{
    public class CitiesQueryHandler : IRequestHandler<CitiesQuery, IEnumerable<string>>
    {
        private readonly IUserService _userService;
        public CitiesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<string>> Handle(CitiesQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetCities(cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
