using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogIn
{
    public record UserLoginCommand(string Email, string Password) : IRequest<LoggedUserDto?>;
}
