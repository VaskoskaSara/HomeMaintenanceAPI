using homeMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Ports.In
{
    public interface IUserService
    {
        Task<LoggedUser?> RegistrationAsync(User user, CancellationToken cancellationToken = default);
        Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default);
        Task<LoggedUser> LoginAsync(User user, CancellationToken cancellationToken = default);
        Task<bool?> UploadImageToS3(IFormFile file); 
        Task<IEnumerable<EmployeeDto>> GetEmployees(string[]? cities, int? price, int? experience, bool? excludeByContract,Guid[] categoryIds, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>?> GetCities(CancellationToken cancellationToken = default);
        Task<UserDetails?> GetEmployeeById(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingInfo?>> GetBookingsByEmployee(Guid id, CancellationToken cancellationToken = default);
        Task<bool> PostAvaliabilty(EmployeeDisableDates employeeDisableDates, CancellationToken cancellationToken = default);
        Task<List<DateOnly>> GetDisabledDatesByEmployee(Guid id, CancellationToken cancellationToken = default);
        Task<List<DateTime>> GetBookedDatesByEmployee(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingInfo?>> GetBookingsByUser(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AddReview(UserReview user, CancellationToken cancellationToken = default);
        Task<List<UserReviewsDto?>> GetReviewsByUser(Guid id, CancellationToken cancellationToken = default);
    }
}
