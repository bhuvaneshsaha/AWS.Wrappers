
namespace AWS.S3.Wrapper.Implementation;
public class LargeFileOperations(IAmazonS3 s3Client) : ILargeFileOperations
{
    public async Task UploadFullDirectoryAsync(string bucketName, string keyPrefix, string localPath, CancellationToken cancellationToken)
    {
        var transferUtil = new TransferUtility(s3Client);
        if (Directory.Exists(localPath))
        {
            await transferUtil.UploadDirectoryAsync(new TransferUtilityUploadDirectoryRequest
            {
                BucketName = bucketName,
                KeyPrefix = keyPrefix,
                Directory = localPath,
            }, cancellationToken);
        }
        else
        {
            throw new ArgumentException("Directory does not exist");
        }
    }

}
