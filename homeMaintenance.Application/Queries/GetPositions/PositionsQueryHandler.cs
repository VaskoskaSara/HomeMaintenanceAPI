using AutoMapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class PositionsQueryHandler : IRequestHandler<PositionsQuery, List<PositionDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PositionsQueryHandler(IUserService userService,IMapper mapper) {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<PositionDto>> Handle(PositionsQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetPositions(cancellationToken).ConfigureAwait(false);
            return response != null ? _mapper.Map<List<PositionDto>>(response) : [];
        }
    }
}
