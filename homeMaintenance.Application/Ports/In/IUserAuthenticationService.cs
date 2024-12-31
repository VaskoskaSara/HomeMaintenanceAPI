using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<LoggedUserDto> AuthenticateUserAsync(User loginUser, CancellationToken cancellationToken = default);
    }
}
