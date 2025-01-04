using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.EmployeeDisabledDates
{
    public class EmployeeDisabledDatesCommandHandler : IRequestHandler<EmployeeDisabledDates, bool>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EmployeeDisabledDatesCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EmployeeDisabledDates request, CancellationToken cancellationToken)
        {
            var disableDates = _mapper.Map<EmployeeDisableDates>(request);
            var result = await _userService.PostAvaliabilty(disableDates, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
