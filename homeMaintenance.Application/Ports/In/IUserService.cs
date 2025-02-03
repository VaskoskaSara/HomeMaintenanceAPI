using homeMaintenance.Domain.Entities;
using homeMaintenance.Application.DTOs;

namespace homeMaintenance.Application.Ports.In
{
    public interface IUserService
    {
        Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default);
        Task<IEnumerable<string>?> GetCities(CancellationToken cancellationToken = default);
        Task<bool> PostAvaliabilty(DisabledDatesByEmployee employeeDisableDates, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingInfoDto?>> GetBookingsByUser(Guid id, CancellationToken cancellationToken = default);
    }
}
