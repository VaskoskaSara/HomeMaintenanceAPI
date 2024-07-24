using Amazon.S3.Model;
using Amazon.S3;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace homeMaintenance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ITransactionWrapper _transactionWrapper;
        private readonly IUserRepository _userRepository;

        public UserService(ITransactionWrapper transactionWrapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<long?> RegistrationAsync(User user, CancellationToken cancellationToken = default)
        {
            if (!IsValidStrongPassword(user.Password))
            {
                throw new ValidationException("Password is not string enough");
            }

            var userByEmail = await _userRepository.GetUserByEmail(user.Email);
            
            if(userByEmail != null)
            {
                throw new ValidationException("User with that email already exists");
            }

            byte[] salt = GenerateSalt();
            string hashedPassword = HashPassword(user.Password, salt);

            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new Exception("Error was occured while saving password");
            }

            user.Password = hashedPassword;

            var response = await _userRepository.RegisterUser(user);
            return response;
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
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Hash the concatenated password and salt
                    byte[] hashedBytes = sha256Hash.ComputeHash(saltedPassword);

                    // Concatenate the salt and hashed password for storage
                    byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                    Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                    Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                    hashedString = Convert.ToBase64String(hashedPasswordWithSalt);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return hashedString;
        }

        public async Task<bool> LoginAsync(User loginUser, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var user = await _userRepository.GetUserByEmail(loginUser.Email);

            if (user != null && VerifyPassword(loginUser.Password, user.Password))
            {
                return true;
            }

            return false;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return password == passwordHash;
        }

        //mozhda postions ne se za vo userRepo
        public async Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default)
        {
            // zosho mi e unit of work
            //IEnumerable<Position>? positions = null;
            //positions = await _userRepository.GetPositionsAsync();

            //return positions;
            return await _userRepository.GetPositionsAsync();
        }

        public async Task<bool?> UploadImageToS3(IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                throw new FormatException("Only images are accepted");
            }

            AmazonS3Client client = _userRepository.GetAwsClient();

            byte[] fileBytes = new byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

            var fileName = file.FileName;
            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = "live-demo-bucket821",
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.NoACL
                };

                response = await client.PutObjectAsync(request);
            }
;

            //if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    // this model is up to you, i have saved a reference to my database 
            //    // to connect the images with users etc.
            //    AddImageReferenceCommand addImageReferenceCommand = new AddImageReferenceCommand
            //    {
            //        Bucket = command.Bucket,
            //        Key = fileName,
            //        Name = fileName
            //    };
            //    // adds the image reference
            //    imageDto = await _userService.AddImage(addImageReferenceCommand);
            //}
            //else
            //{
            //    // do some error handling
            //}

            return true;
        }

    }
}
