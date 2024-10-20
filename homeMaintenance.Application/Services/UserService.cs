using Amazon.S3.Model;
using Amazon.S3;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Amazon;
using Amazon.Runtime;
using Konscious.Security.Cryptography;
using AutoMapper;
using homeMaintenance.Domain.Enum;
using homeMaintenance.Domain.Exceptions;
using System.Net;
using System.Security.Authentication;

namespace homeMaintenance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ITransactionWrapper _transactionWrapper;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(ITransactionWrapper transactionWrapper, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<LoggedUser?> RegistrationAsync(User user, CancellationToken cancellationToken = default)
        {
            if (!IsValidStrongPassword(user.Password))
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Password is not string enough");
            }

            var userByEmail = await _userRepository.GetUserByEmailAsync(user.Email);

            if (userByEmail != null)
            {
                throw new CustomException(HttpStatusCode.Conflict, "User with that email already exists");
            }

            string hashedPassword = HashPassword(user.Password, GenerateSalt());

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new CustomException(HttpStatusCode.InternalServerError, "Error was occured while saving password");
            }

            user.Password = hashedPassword;

            if (user.UserType != UserType.Customer) {

                if (user.PositionId == null)
                {
                    if (string.IsNullOrEmpty(user.NewPosition))
                    {
                        throw new Exception("Position must be entered");
                    }

                    else
                    {
                        var positions = await _userRepository.GetPositionsAsync();
                        bool exists = positions.Any(item => string.Equals(item.PositionName, user.NewPosition, StringComparison.OrdinalIgnoreCase));

                        if (!exists)
                        {
                            var newPosition = await _userRepository.InsertPosition(user.NewPosition);
                            user.PositionId = newPosition;
                        }
                    }
                }

                if (user.PaymentType != PaymentType.excludeByContract && user.Price == null) {

                    throw new Exception("Price must be entered");
                }
            }

            var response = await _userRepository.RegisterUser(user);

            string imagePath;

            if (response.Avatar != null)
            {
                imagePath = GetImageFromAws(response.Avatar);
            }
            else
            {
                imagePath = GetImageFromAws("defaultUser.jpg");
            }

            return new LoggedUser
            {
                Id = response.Id,
                UserRole = response.UserRole,
                Avatar = imagePath
            };
        }

        public bool IsValidStrongPassword(string password)
        {
            if (password.Length < 8 || (!Regex.IsMatch(password, @"[0-9]")) || (!Regex.IsMatch(password, @"[a-z]")) ||
                (!Regex.IsMatch(password, @"[A-Z]")) || (!Regex.IsMatch(password, @"[@$%*?&#]")))
            {
                return false;
            }

            return true;
        }

        static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }


        private string? HashPassword(string password, byte[] salt)
        {
            string hashedString = null;

            try
            {
                using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
                {
                    Salt = salt,
                    Iterations = 4,
                    MemorySize = 8 * 1024,
                    DegreeOfParallelism = 8
                };
                var hashBytes = hasher.GetBytes(32);
                var saltedHash = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, saltedHash, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, saltedHash, salt.Length, hashBytes.Length);
                hashedString = Convert.ToBase64String(saltedHash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return hashedString;
        }

        public async Task<LoggedUser> LoginAsync(User loginUser, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var user = await _userRepository.GetUserByEmailAsync(loginUser.Email);

            if (user != null && VerifyPassword(loginUser.Password, user.Password))
            {
                string imagePath;

                if (user.Avatar != null)
                {
                    imagePath = GetImageFromAws(user.Avatar);
                }
                else
                {
                    imagePath = GetImageFromAws("defaultUser.jpg");
                }

                return new LoggedUser
                {
                    Id = user.Id,
                    UserRole = user.UserRole,
                    Avatar = imagePath
                };
            }

            throw new InvalidCredentialException();
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            var storedHashBytes = Convert.FromBase64String(passwordHash);
            var salt = new byte[16];
            var hash = new byte[storedHashBytes.Length - salt.Length];

            Buffer.BlockCopy(storedHashBytes, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(storedHashBytes, salt.Length, hash, 0, hash.Length);

            using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                Iterations = 4,
                MemorySize = 8 * 1024,
                DegreeOfParallelism = 8
            };
            var computedHash = hasher.GetBytes(32);
            return hash.SequenceEqual(computedHash);
        }

        public async Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetPositionsAsync();
        }

        public async Task<bool?> UploadImageToS3(IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                throw new FormatException("Only images are accepted");
            }

            var credentials = new BasicAWSCredentials("AKIA2UC26MP7ZI263NKL", "AAZtoZ0Tiuji2qVo39VvOa1I6ut7ddnX6qTyuCIX");
            AmazonS3Client s3Client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);

            byte[] fileBytes = new byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

            var fileName = file.FileName;
            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = "homemaintenanceapp",
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.NoACL
                };

                response = await s3Client.PutObjectAsync(request);
            }

            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetEmployeesAsync(cities, price, experience, excludeByContract, categoryIds);

            foreach (var employee in response)
            {
                string imagePath;

                if (employee.Avatar != null)
                {
                    imagePath = GetImageFromAws(employee.Avatar);
                }
                else
                {
                    imagePath = GetImageFromAws("defaultUser.jpg");
                }

                employee.Avatar = imagePath;
            }

            return _mapper.Map<IEnumerable<EmployeeDto>>(response);
        }


        private string GetImageFromAws (string key)
        {
            var credentials = new BasicAWSCredentials("AKIA2UC26MP7ZI263NKL", "AAZtoZ0Tiuji2qVo39VvOa1I6ut7ddnX6qTyuCIX");
            AmazonS3Client s3Client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "homemaintenanceapp",
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(30)
            };

            string url = s3Client.GetPreSignedURL(request);

            return url;
        }

        public async Task<IEnumerable<string>?> GetCities(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetCitiesAsync();
        }

        public async Task<UserDetails?> GetEmployeeById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetEmployeeByIdAsync(id);

            string imagePath;
            List<string> images = new List<string>();

            if (response.Avatar != null)
            {
                imagePath = GetImageFromAws(response.Avatar);
            }
            else
            {
                imagePath = GetImageFromAws("defaultUser.jpg");
            }

            if (response.Photos.Any())
            {
                response.Photos.ForEach(x =>
                {
                    images.Add(GetImageFromAws(x));
                });
            }

            response.Avatar = imagePath;
            response.Photos = images;
            return _mapper.Map<UserDetails>(response);
        }

        public async Task<IEnumerable<BookingInfo?>> GetBookingsByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetBookingsByEmployeeAsync(id);

            string imagePath;
            List<string> images = new List<string>();

            foreach (var item in response)
            {
            if (item.Avatar != null)
            {
                imagePath = GetImageFromAws(item.Avatar);
            }
            else
            {
                imagePath = GetImageFromAws("defaultUser.jpg");
            }
                item.Avatar = imagePath;
            }

            return _mapper.Map<IEnumerable<BookingInfo>>(response);
        }

        public async Task<bool> PostAvaliabilty(EmployeeDisableDates employeeDisableDates, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.PostAvaliability(employeeDisableDates);

            return result;
        }

        public async Task<List<DateOnly>> GetDisabledDatesByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.GetDisabledDatesByEmployeeAsync(id);

            return result;
        }

        public async Task<List<DateTime>> GetBookedDatesByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var bookings = await _userRepository.GetBookingsByEmployeeAsync(id);

            var allBookingDates = bookings.Select(c => (c.StartDateTime, c.EndDateTime))
                        .SelectMany(b => Enumerable.Range(0, (b.EndDateTime - b.StartDateTime).Days + 1).Select(d => b.StartDateTime.AddDays(d)))
                        .Distinct()
                        .ToList();

            return allBookingDates;
        }

        public async Task<IEnumerable<BookingInfo?>> GetBookingsByUser(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetBookingsByUserAsync(id);

            string imagePath;
            List<string> images = new List<string>();

            foreach (var item in response)
            {
                if (item.Avatar != null)
                {
                    imagePath = GetImageFromAws(item.Avatar);
                }
                else
                {
                    imagePath = GetImageFromAws("defaultUser.jpg");
                }
                item.Avatar = imagePath;
            }

            return _mapper.Map<IEnumerable<BookingInfo>>(response);
        }
    }
}
