using Amazon.S3;
using Dapper;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using HomeMaintenanceApp.Web;
using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AmazonS3Client _awsClient;

        public UserRepository(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
            _awsClient = dbConfig.GetAwsClient();
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            var response = await _dbConnection.QueryAsync<Position>("GetPositions", commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<UserLoginDto> GetUserByEmail(string email)
        {
           return await _dbConnection.QueryFirstOrDefaultAsync<UserLoginDto>("GetUserByEmail", new
            {
                Email = email
            }, commandType: CommandType.StoredProcedure);

        }

        public async Task<long?> RegisterUser(User user)
        {
            var response = await _dbConnection.ExecuteScalarAsync<long>("InsertUser",
                new
                {
                    user.FullName,
                    user.PhoneNumber,
                    user.Email,
                    user.Password,
                    user.City,
                    UserRole = (int)user.UserType,
                    user.Experience,
                    user.Price,
                    user.BirthDate,
                    user.PositionId,
                    user.Avatar
                },
                commandType: CommandType.StoredProcedure);
            return response;
        }

        public AmazonS3Client GetAwsClient()
        {
            return _awsClient;
        }

        public async Task<IEnumerable<User>> GetEmployeesAsync()
        {
            var response = await _dbConnection.QueryAsync<User>("GetEmployees", commandType: CommandType.StoredProcedure);

            return response;
        }
    }
}
