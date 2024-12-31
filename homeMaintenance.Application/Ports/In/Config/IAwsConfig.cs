using Amazon.S3;

public interface IAwsConfig
{
    string AwsAccessKey { get; }
    string AwsSecretKey { get; }
    AmazonS3Client GetAwsClient();
}
