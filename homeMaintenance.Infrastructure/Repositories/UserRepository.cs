﻿using Amazon.S3;
using Dapper;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
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

        public async Task<Guid?> RegisterUser(User user)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@FullName", user.FullName);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@City", user.City);
            parameters.Add("@UserRole", (int)user.UserType);
            parameters.Add("@Experience", user.Experience);
            parameters.Add("@Price", user.Price);
            parameters.Add("@BirthDate", user.BirthDate);
            parameters.Add("@PositionId", user.PositionId);
            parameters.Add("@Avatar", user.Avatar);
            parameters.Add("@PaymentType", user.PaymentType);
            parameters.Add("@NumberOfEmployees", user.NumberOfEmployees);
            parameters.Add("@Description", user.Description);

            parameters.Add("@NewId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("InsertUser", parameters, commandType: CommandType.StoredProcedure);

            var newUserId = parameters.Get<Guid>("@NewId");


            var photoParameters = user.Photos?.Select(name => new { Image = name, ImageOrigin = ImageOrigin.User, UserId = user.Id }).ToList();

            int rows = await _dbConnection.ExecuteAsync("InsertImages",
                photoParameters,
                commandType: CommandType.StoredProcedure);

            return newUserId;
        }

        public AmazonS3Client GetAwsClient()
        {
            return _awsClient;
        }

        public async Task<IEnumerable<User>> GetEmployeesAsync(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds)
        {
            var response = await _dbConnection.QueryAsync<User>("GetEmployees",
                new
                {
                    @Cities = (cities != null && cities.Length > 0) ? string.Join(",", cities) : null,
                    price,
                    experience,
                    excludeByContract = (excludeByContract == null || excludeByContract == false) ? 0 : 1,
                    @CategoryIds = categoryIds.Length > 0 ? string.Join(",", categoryIds) : null
                },
                commandType: CommandType.StoredProcedure);

           return response;
        }

        public async Task<Guid> InsertPosition(string newPosition)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@PositionName", newPosition);
            parameters.Add("@NewId", dbType: DbType.Guid, direction: ParameterDirection.Output);


            await _dbConnection.ExecuteScalarAsync<Guid>("InsertPosition", parameters, commandType: CommandType.StoredProcedure);

            var response = parameters.Get<Guid>("@NewId");

            return response;
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            var response = await _dbConnection.QueryAsync<string>("GetCities", commandType: CommandType.StoredProcedure);
            return response;
        }

        public async Task<UserDetailsDto?> GetEmployeeById(Guid id)
        {

            var response = await _dbConnection.QueryMultipleAsync("GetEmployeeById",
                new
                {
                    id
                },
                commandType: CommandType.StoredProcedure);

            if(response == null) return null;

            var userDetails = await response.ReadSingleOrDefaultAsync<UserDetailsDto>();
            var userImages = await response.ReadAsync<UserImage>();

            if (userDetails != null)
            {
                userDetails.Photos = userImages.Select(img => img.Image).ToList();
            }

            return userDetails;
        }
    }
}
