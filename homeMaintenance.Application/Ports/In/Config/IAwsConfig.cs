using Amazon.S3;

namespace homeMaintenance.Application.Ports.In.Config
{
    public interface IAwsConfig
    {
        string AwsAccessKey { get; }
        string AwsSecretKey { get; }
        AmazonS3Client GetAwsClient();
        string GetBucketName();
    }
}