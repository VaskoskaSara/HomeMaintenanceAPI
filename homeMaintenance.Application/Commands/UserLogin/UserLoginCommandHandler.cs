using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoggedUser>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public UserLoginCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<LoggedUser> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var result = await _serviceContainer.UserService.LoginAsync(user, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
