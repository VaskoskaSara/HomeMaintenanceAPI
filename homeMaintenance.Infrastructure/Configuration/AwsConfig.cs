using Amazon.S3;
using Microsoft.Extensions.Configuration;

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
        var client = new AmazonS3Client(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.EUCentral1);
        return client;
    }
}
