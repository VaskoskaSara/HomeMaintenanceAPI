using Amazon.S3;
using Dapper;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
using HomeMaintenanceApp.Web;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;

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

        public async Task<UserLoginDto> GetUserByEmailAsync(string email)
        {
           return await _dbConnection.QueryFirstOrDefaultAsync<UserLoginDto>("GetUserByEmail", new
            {
                Email = email
            }, commandType: CommandType.StoredProcedure);

        }

        public async Task<UserLoginDto?> RegisterUser(User user)
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


            var photoParameters = user.Photos?.Select(name => new { Image = name, ImageOrigin = ImageOrigin.User, UserId = newUserId, EmployeeId = (Guid?)null }).ToList();

            int rows = await _dbConnection.ExecuteAsync("InsertImages",
                photoParameters,
                commandType: CommandType.StoredProcedure);

            return new UserLoginDto
            {
                Id = newUserId,
                UserRole = user.UserType,
                Avatar = user.Avatar
            };
        }

        public AmazonS3Client GetAwsClient()
        {
            return _awsClient;
        }

        public async Task<IList<User>> GetEmployeesAsync(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds)
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

           return response.ToList();
        }

        public async Task<IEnumerable<int>> GetRatingByEmployeeId(Guid id)
        {
            var response = await _dbConnection.QueryAsync<int>("GetRatingsByEmployeeId",
              new
              {
                  @Id = id,

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

        public async Task<UserDetailsDto?> GetEmployeeByIdAsync(Guid id)
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

        public async Task<IEnumerable<BookingInfoDto?>> GetBookingsByEmployeeAsync(Guid id)
        {
            var response = await _dbConnection.QueryAsync<BookingInfoDto>("GetBookingsByEmployee",
               new
               {
                   id
               },
               commandType: CommandType.StoredProcedure);

            return response;
        }

        public async Task<bool> PostAvaliability(EmployeeDisableDates employeeDisableDates)
        {
            var combinedDistinct = new List<DateTime>{ };
            var disabledEmployeeByUser = await GetDisabledDatesByEmployeeAsync(employeeDisableDates.UserId);

            if (employeeDisableDates.IsEnabled == true)
            {
                combinedDistinct = employeeDisableDates.DisabledDates.ToList();
            }
            else
            {
                combinedDistinct = employeeDisableDates.DisabledDates
               .Select(dt => dt)
               .Concat(disabledEmployeeByUser.Select(it => it.ToDateTime(TimeOnly.MinValue)))
               .Distinct()
               .ToList();
            }

            var response = await _dbConnection.ExecuteAsync("InsertEmployeeDisabledDates",
              new
              {
                  id = employeeDisableDates.UserId,
                  disabledDates = string.Join(",", combinedDistinct)
              },
              commandType: CommandType.StoredProcedure);

            return response == -1;
        }

        public async Task<List<DateOnly>> GetDisabledDatesByEmployeeAsync(Guid id)
        {
            var response = await _dbConnection.QueryFirstOrDefaultAsync<string>("GetDisabledDatesByEmployeeId",
            new
            {
                id
            },
            commandType: CommandType.StoredProcedure);
            var dateList = new List<DateOnly>();

            if (!response.IsNullOrEmpty())
            {
                var dateStrings = response.Split(',');
                var dateFormat = "dd/MM/yyyy HH:mm:ss";

                foreach (var dateString in dateStrings)
                {
                    if (DateOnly.TryParseExact(dateString.Trim(), dateFormat,
                                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
                    {
                        dateList.Add(date);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid date format: {dateString}");
                    }
                }
            }
            return dateList;
        }

        public async Task<IEnumerable<BookingInfoDto?>> GetBookingsByUserAsync(Guid id)
        {
            var response = await _dbConnection.QueryAsync<BookingInfoDto>("GetBookingsByUser",
              new
              {
                  id
              },
              commandType: CommandType.StoredProcedure);

            return response;
        }

        public async Task<bool> AddReview(UserReview review)
        {

            var response = await _dbConnection.ExecuteAsync("InsertReview",
            new
            {
                review.Comment,
                review.UserId,
                review.Rating,
                review.EmployeeId,
                PaymentId = review.PaymentId == "null" ? null : review.PaymentId,
              },
              commandType: CommandType.StoredProcedure);

            var photoParameters = review.Photos?.Select(name => new { Image = name, ImageOrigin = ImageOrigin.Customer, review.UserId, review.EmployeeId }).ToList();

            int rows = await _dbConnection.ExecuteAsync("InsertImages",
                photoParameters,
                commandType: CommandType.StoredProcedure);

            return response > 0;
        }

        public async Task<List<UserReviewsDto?>> GetReviewsByUserAsync(Guid id)
        {
           var response = await _dbConnection.QueryAsync<UserReviews>("GetReviewsByUserId",
           new
           {
               id
           },
           commandType: CommandType.StoredProcedure);

            var groupedReviews = response
            .GroupBy(r => new { r.UserId, r.PaymentId, r.Avatar, r.FullName, r.Comment, r.Rating })
            .Select(g => new UserReviewsDto
            {
                UserId = g.Key.UserId,
                FullName = g.Key.FullName,
                Avatar = g.Key.Avatar,
                Comment = g.Key.Comment,
                Ratings = g.Key.Rating,
                Photos = g.Any(x => x.Photo != null) ? g.Select(x => x.Photo).ToList() : null,
            }).ToList();

            return groupedReviews;
        }
    }
}
