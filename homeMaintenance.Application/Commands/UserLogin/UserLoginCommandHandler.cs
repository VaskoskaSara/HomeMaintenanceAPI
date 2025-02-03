using AutoMapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogIn
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoggedUserDto?>
    {
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public UserLoginCommandHandler(IUserAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<LoggedUserDto?> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var result = await _authenticationService.AuthenticateUserAsync(user, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
