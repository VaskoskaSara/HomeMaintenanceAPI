using homeMaintenance.Application.DTOs;

namespace homeMaintenance.Application.Ports.In
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds, CancellationToken cancellationToken = default);
        Task<UserDetailsDto?> GetEmployeeById(Guid id, CancellationToken cancellationToken = default);
        Task<List<DateOnly>> GetDisabledDatesByEmployee(Guid id, CancellationToken cancellationToken = default);
        Task<List<DateTime>> GetBookedDatesByEmployee(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingInfoDto?>> GetBookingsByEmployee(Guid id, CancellationToken cancellationToken = default);
    }
}
