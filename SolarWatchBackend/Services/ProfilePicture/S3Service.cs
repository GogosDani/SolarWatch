using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace SolarWatch.Services.ProfilePicture;

public class S3Service : IS3Service
{

    private readonly string _bucketName;
    private readonly IAmazonS3 _client;
    
    public S3Service(IConfiguration configuration)
    {
        _bucketName = configuration["AwsBucketName"];
        _client = new AmazonS3Client(
            configuration["AwsAccessKey"],
            configuration["AwsSecretKey"],
            RegionEndpoint.GetBySystemName(configuration["AwsRegion"])
        );
    }
    
    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = fileName,
            BucketName = _bucketName,
            CannedACL = S3CannedACL.Private
        };
        var fileTransferUtility = new TransferUtility(_client);
        await fileTransferUtility.UploadAsync(uploadRequest);
        return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
    }
}