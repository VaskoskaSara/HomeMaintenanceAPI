using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using homeMaintenance.Application.Ports.In;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace homeMaintenance.Application.Services
{
    public class AwsImageStorageService : IImageStorageService
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public AwsImageStorageService(IConfiguration configuration)
        {
            var awsAccessKey = configuration["AwsConfiguration:AWSAccessKey"];
            var awsSecretKey = configuration["AwsConfiguration:AWSSecretKey"];
            _bucketName = configuration["AwsConfiguration:BucketName"];

            var credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
            _s3Client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);
        }

        public string GetImageUrl(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(30)
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                throw new FormatException("Only images are accepted.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new FormatException("Unsupported image format.");
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = uniqueFileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.NoACL
                };

                try
                {
                    var response = await _s3Client.PutObjectAsync(request);
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return uniqueFileName;
                    }
                    else
                    {
                        throw new Exception($"Error uploading file to S3: {response.HttpStatusCode}");
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    throw new Exception($"AWS S3 error: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error uploading file: {ex.Message}", ex);
                }
            }
        }

        public string GetImagePath(string? avatarKey)
        {
            return string.IsNullOrEmpty(avatarKey) ? GetImageUrl("defaultUser.jpg") : GetImageUrl(avatarKey);
        }
    }
}
