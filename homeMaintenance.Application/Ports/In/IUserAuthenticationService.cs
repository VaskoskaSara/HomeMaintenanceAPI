using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.In
{
    public interface IUserAuthenticationService
    {
        Task<LoggedUserDto?> AuthenticateUserAsync(User loginUser, CancellationToken cancellationToken = default);
    }
}
