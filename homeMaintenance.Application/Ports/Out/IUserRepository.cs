using Amazon.S3;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<long?> RegisterUser(User user);
        Task<UserLoginDto> GetUserByEmail(string email);
        AmazonS3Client GetAwsClient();
        Task<IEnumerable<User>> GetEmployeesAsync(string[]? city, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds);
        Task<Guid> InsertPosition(string newPosition);
        Task<IEnumerable<string>> GetCitiesAsync();

    }
}
