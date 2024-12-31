using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class PositionsQueryHandler : IRequestHandler<PositionsQuery, IEnumerable<Position>>
    {
        private readonly IUserService _userService;
        public PositionsQueryHandler(IUserService userService) {
            _userService = userService;
        }

        public async Task<IEnumerable<Position>> Handle(PositionsQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetPositions(cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
