
namespace AWS.S3.Wrapper.Implementation;
public class LargeFileOperations : ILargeFileOperations
{
    private readonly IAmazonS3 _s3Client;

    public LargeFileOperations(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }
    public Task AbortMultipartUploadAsync(string bucketName, string objectKey, string uploadId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UploadFullDirectoryAsync(string bucketName, string keyPrefix, string localPath, CancellationToken cancellationToken)
    {
        var transferUtil = new TransferUtility(_s3Client);
        if (Directory.Exists(localPath))
        {
            try
            {
                await transferUtil.UploadDirectoryAsync(new TransferUtilityUploadDirectoryRequest
                {
                    BucketName = bucketName,
                    KeyPrefix = keyPrefix,
                    Directory = localPath,
                }, cancellationToken);

                return true;
            }
            catch (AmazonS3Exception s3Ex)
            {
                Console.WriteLine($"Can't upload the contents of {localPath} because:");
                Console.WriteLine(s3Ex.Message);
                return false;
            }
        }
        else
        {
            Console.WriteLine($"The directory {localPath} does not exist.");
            return false;
        }
    }

    // TODO Need to implement
    public Task UploadLargeFileMultipartAsync(string bucketName, string objectKey, Stream inputStream, long partSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
