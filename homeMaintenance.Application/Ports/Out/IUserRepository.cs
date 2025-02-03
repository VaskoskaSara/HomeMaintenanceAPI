using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<UserLogin?> RegisterUser(User user);
        Task<UserLogin> GetUserByEmailAsync(string email);
        Task<IList<User>> GetEmployeesAsync(string[]? city, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds);
        Task<Guid> InsertPosition(string newPosition);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<UserDetailsDto?> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<BookingInfo>> GetBookingsByEmployeeAsync(Guid id);
        Task<bool> PostAvaliability(DisabledDatesByEmployee employeeDisableDates);
        Task<List<DateOnly>> GetDisabledDatesByEmployeeAsync(Guid id);
        Task<IEnumerable<BookingInfo>> GetBookingsByUserAsync(Guid id);
        Task<bool> AddReview(AddUserReview user);
        Task<IEnumerable<int>> GetRatingByEmployeeId(Guid id);
        Task<IEnumerable<UserReview?>> GetReviewsByUserAsync(Guid id);
        Task InsertUserPhotosAsync(IEnumerable<string> photos, Guid userId);
        Task<IEnumerable<string>> GetEmployeeNameById(Guid id);
    }
}
