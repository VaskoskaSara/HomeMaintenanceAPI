using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public record UserLoginCommand : IRequest<LoggedUserDto?>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
