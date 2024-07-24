using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class PositionsQueryHandler : IRequestHandler<PositionsQuery, IEnumerable<Position>>
    {
        private readonly IServiceContainer _serviceContainer;
        public PositionsQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<IEnumerable<Position>> Handle(PositionsQuery request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetPositions(cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
