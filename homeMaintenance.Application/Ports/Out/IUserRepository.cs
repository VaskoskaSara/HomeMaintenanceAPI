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
        Task<IEnumerable<User>> GetEmployeesAsync();

    }
}
