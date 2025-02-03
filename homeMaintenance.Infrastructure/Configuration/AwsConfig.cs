using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using homeMaintenance.Application.Ports.In.Config;
using Microsoft.Extensions.Configuration;

namespace homeMaintenance.Infrastructure.Configuration
{
    public class AwsConfig : IAwsConfig
    {
        private readonly IConfiguration _configuration;

        public AwsConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AwsAccessKey => _configuration["AwsConfiguration:AWSAccessKey"];
        public string AwsSecretKey => _configuration["AwsConfiguration:AWSSecretKey"];

        public AmazonS3Client GetAwsClient()
        {
            var credentials = new BasicAWSCredentials(AwsAccessKey, AwsSecretKey);
            var client = new AmazonS3Client(credentials, RegionEndpoint.USEast1);
            return client;
        }

        public string GetBucketName()
        {
            return _configuration["AwsConfiguration:BucketName"];
        }
    }
}