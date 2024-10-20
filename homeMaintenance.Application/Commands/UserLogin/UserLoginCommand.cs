using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public record UserLoginCommand : IRequest<LoggedUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
