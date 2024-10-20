using Amazon.S3;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<UserLoginDto?> RegisterUser(User user);
        Task<UserLoginDto> GetUserByEmailAsync(string email);
        AmazonS3Client GetAwsClient();
        Task<IEnumerable<User>> GetEmployeesAsync(string[]? city, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds);
        Task<Guid> InsertPosition(string newPosition);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task<UserDetailsDto?> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<BookingInfoDto?>> GetBookingsByEmployeeAsync(Guid id);
        Task<bool> PostAvaliability(EmployeeDisableDates employeeDisableDates);
        Task<List<DateOnly>> GetDisabledDatesByEmployeeAsync(Guid id);
        Task<IEnumerable<BookingInfoDto?>> GetBookingsByUserAsync(Guid id);
        Task<bool> AddReview(UserReview user);

    }
}
