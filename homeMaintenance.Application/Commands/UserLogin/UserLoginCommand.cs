using MediatR;

namespace homeMaintenance.Application.Commands.UserLogin
{
    public record UserLoginCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
