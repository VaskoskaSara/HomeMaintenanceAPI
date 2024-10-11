using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public class EmployeeDisabledDatesCommandHandler : IRequestHandler<EmployeeDisabledDates, bool>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public EmployeeDisabledDatesCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EmployeeDisabledDates request, CancellationToken cancellationToken)
        {
            var disableDates = _mapper.Map<EmployeeDisableDates>(request);
            var result = await _serviceContainer.UserService.PostAvaliabilty(disableDates, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
