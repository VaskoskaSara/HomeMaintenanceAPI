using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<LoggedUserDto> RegisterUserAsync(User user, CancellationToken cancellationToken = default);
    }
}
