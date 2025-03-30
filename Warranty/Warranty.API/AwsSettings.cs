namespace Warranty.API
{
    public class AwsSettings
    {
        public string AccessKey { get; set; } = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
        public string SecretKey { get; set; } = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
        public string Region { get; set; } = Environment.GetEnvironmentVariable("AWS_REGION");
    }
}