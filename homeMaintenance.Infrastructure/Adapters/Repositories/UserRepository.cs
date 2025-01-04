using Amazon.S3;
using Dapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
using homeMaintenance.Infrastructure.Adapters.Db;
using System.Data;
using System.Globalization;

namespace homeMaintenance.Infrastructure.Adapters.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbHelper _dbHelper;
        private readonly AmazonS3Client _awsClient;

        public UserRepository(IDbHelper dbHelper, IAwsConfig awsConfig)
        {
            _dbHelper = dbHelper;
            _awsClient = awsConfig.GetAwsClient();
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            return await _dbHelper.ExecuteQueryAsync<Position>("GetPositions");
        }

        public async Task<UserLogin> GetUserByEmailAsync(string email)
        {
            var user = await _dbHelper.ExecuteQueryAsync<UserLogin>("GetUserByEmail", new { Email = email });
            return user.FirstOrDefault();
        }

        public async Task<UserLogin?> RegisterUser(User user)
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
            parameters.Add("@Address", user.Address);
            parameters.Add("@NewId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _dbHelper.ExecuteAsync("InsertUser", parameters);
            var newUserId = parameters.Get<Guid>("@NewId");

            return new UserLogin
            {
                Id = newUserId,
                UserRole = user.UserType,
                Avatar = user.Avatar
            };
        }

        public async Task InsertUserPhotosAsync(IEnumerable<string>? photos, Guid userId)
        {
            if (photos?.Any() == true)
            {
                var photoParameters = photos.Select(name => new { Image = name, ImageOrigin = ImageOrigin.User, UserId = userId, EmployeeId = (Guid?)null, ReviewId = (Guid?)null }).ToList();
                await _dbHelper.ExecuteAsync("InsertImages", photoParameters);
            }
        }

        public async Task<IList<User>> GetEmployeesAsync(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds)
        {
            var response = await _dbHelper.ExecuteQueryAsync<User>("GetEmployees",
                new
                {
                    @Cities = cities != null && cities.Length > 0 ? string.Join(",", cities) : null,
                    price,
                    experience,
                    excludeByContract = excludeByContract == null || excludeByContract == false ? 0 : 1,
                    @CategoryIds = categoryIds.Length > 0 ? string.Join(",", categoryIds) : null
                });

            return response.ToList();
        }

        public async Task<IEnumerable<int>> GetRatingByEmployeeId(Guid id)
        {
            var parameters = new { Id = id };
            return await _dbHelper.ExecuteQueryAsync<int>("GetRatingsByEmployeeId", parameters);
        }

        public async Task<Guid> InsertPosition(string newPosition)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@PositionName", newPosition);
            parameters.Add("@NewId", dbType: DbType.Guid, direction: ParameterDirection.Output);


            await _dbHelper.ExecuteScalarAsync<Guid>("InsertPosition", parameters);

            return parameters.Get<Guid>("@NewId");
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            return await _dbHelper.ExecuteQueryAsync<string>("GetCities");
        }

        public async Task<UserDetailsDto?> GetEmployeeByIdAsync(Guid id)
        {
            var parameters = new { id };

            var response = await _dbHelper.ExecuteQueryMultipleAsync("GetEmployeeById",
                parameters);

            if (response == null) return null;

            var userDetails = await response.ReadSingleOrDefaultAsync<UserDetailsDto>();
            var userImages = await response.ReadAsync<UserImage>();

            if (userDetails != null)
            {
                userDetails.Photos = userImages.Select(img => img.Image).ToList();
            }

            return userDetails;
        }

        public async Task<IEnumerable<BookingInfo>> GetBookingsByEmployeeAsync(Guid id)
        {
            var parameters = new { id };

            return await _dbHelper.ExecuteQueryAsync<BookingInfo>("GetBookingsByEmployee",
               parameters);
        }

        public async Task<bool> PostAvaliability(EmployeeDisableDates employeeDisableDates)
        {
            var disabledEmployeeByUser = await GetDisabledDatesByEmployeeAsync(employeeDisableDates.UserId);

            var combinedDistinct = employeeDisableDates.IsEnabled.HasValue
                ? employeeDisableDates.IsEnabled.Value
                    ? employeeDisableDates.DisabledDates.ToList()
                    : employeeDisableDates.DisabledDates
                        .Select(dt => dt)
                        .Concat(disabledEmployeeByUser.Select(it => it.ToDateTime(TimeOnly.MinValue)))
                        .Distinct()
                        .ToList()
                : new List<DateTime>();

            var parameters = new
            {
                id = employeeDisableDates.UserId,
                disabledDates = string.Join(",", combinedDistinct) // Combine the dates into a single string
            };

            var response = await _dbHelper.ExecuteAsync("InsertEmployeeDisabledDates",
              parameters);

            return response == -1;
        }

        public async Task<List<DateOnly>> GetDisabledDatesByEmployeeAsync(Guid id)
        {
            var response = await _dbHelper.ExecuteQueryAsync<string>("GetDisabledDatesByEmployeeId",
            new
            {
                id
            });

            var dateList = new List<DateOnly>();

            if (string.IsNullOrEmpty(response.FirstOrDefault()))
            {
                return dateList;
            }

            var dateStrings = response.FirstOrDefault().Split(',');
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

            return dateList;
        }

        public async Task<IEnumerable<BookingInfo>> GetBookingsByUserAsync(Guid id)
        {
            return await _dbHelper.ExecuteQueryAsync<BookingInfo>("GetBookingsByUser",
              new
              {
                  id
              });
        }

        public async Task<bool> AddReview(AddUserReview review)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Comment", review.Comment);
            parameters.Add("@UserId", review.UserId);
            parameters.Add("@Rating", review.Rating);
            parameters.Add("@EmployeeId", review.EmployeeId);
            parameters.Add("@PaymentId", review.PaymentId == "null" ? null : review.PaymentId);
            parameters.Add("@UserPaymentId", review.UserPaymentId);
            parameters.Add("@NewId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            var response = await _dbHelper.ExecuteAsync("InsertReview", parameters);

            var newReviewId = parameters.Get<Guid>("@NewId");

            var photoParameters = review.Photos?.Select(name => new { Image = name, ImageOrigin = ImageOrigin.Customer, review.UserId, review.EmployeeId, ReviewId = newReviewId }).ToList();

            await _dbHelper.ExecuteAsync("InsertImages", photoParameters);

            // number of rows
            return response > 0;
        }

        public async Task<IEnumerable<UserReview?>> GetReviewsByUserAsync(Guid id)
        {
            return await _dbHelper.ExecuteQueryAsync<UserReview>("GetReviewsByUserId",
             new
             {
                 id
             });
        }

        public async Task<IEnumerable<string>> GetEmployeeNameById(Guid id)
        {
            return await _dbHelper.ExecuteQueryAsync<string>("GetUserNameById",
            new
            {
                id
            });
        }
    }
}
