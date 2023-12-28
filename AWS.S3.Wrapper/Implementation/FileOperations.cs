// Reference: https://docs.aws.amazon.com/AmazonS3/latest/userguide/example_s3_Scenario_UsingLargeFiles_section.html

namespace AWS.S3.Wrapper.Implementation;
public class FileOperations(IAmazonS3 s3Client) : IFileOperations
{
    public async Task CopyFileAsync(string sourceBucket, string sourceObjectKey, string destinationBucket, string destinationObjectKey, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.CopyObjectRequest
        {
            SourceBucket = sourceBucket,
            SourceKey = sourceObjectKey,
            DestinationBucket = destinationBucket,
            DestinationKey = destinationObjectKey
        };

        await s3Client.CopyObjectAsync(request, cancellationToken);
    }

    public async Task DeleteFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = objectKey
        };

        await s3Client.DeleteObjectAsync(request, cancellationToken);
    }

    public async Task<bool> DoseFileExistAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.GetObjectMetadataRequest
        {
            BucketName = bucketName,
            Key = objectKey
        };

        try
        {
            var response = await s3Client.GetObjectMetadataAsync(request, cancellationToken);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Amazon.S3.AmazonS3Exception ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<Stream> GetFileAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
    {
        // GET FILES FROM S3
        var request = new Amazon.S3.Model.GetObjectRequest
        {
            BucketName = bucketName,
            Key = objectKey
        };

        var response = await s3Client.GetObjectAsync(request, cancellationToken);

        return response.ResponseStream;
    }

    public async Task<IEnumerable<string>> ListObjectsAsync(string bucketName, string prefix, string delimiter, int maxKeys, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.ListObjectsRequest
        {
            BucketName = bucketName,
            Prefix = prefix,
            Delimiter = delimiter,
            MaxKeys = maxKeys
        };

        var response = await s3Client.ListObjectsAsync(request, cancellationToken);

        return response.S3Objects.Select(x => x.Key);
    }

    public async Task UploadFileStreamAsync(string bucketName, string objectKey, Stream content, string contentType, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectKey,
            InputStream = content,
            ContentType = contentType
        };

        await s3Client.PutObjectAsync(request, cancellationToken);
    }

    public async Task<bool> UploadSingleFileAsync(string bucketName, string sourceFileFullPath, string destKey, CancellationToken cancellationToken)
    {
        var transferUtil = new TransferUtility(s3Client);
        if (File.Exists(sourceFileFullPath))
        {
            try
            {
                await transferUtil.UploadAsync(new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    Key = destKey,
                    FilePath = sourceFileFullPath,
                });

                return true;
            }
            catch (AmazonS3Exception s3Ex)
            {
                Console.WriteLine($"Could not upload {sourceFileFullPath} to {destKey} because:");
                Console.WriteLine(s3Ex.Message);
                return false;
            }
        }
        else
        {
            Console.WriteLine($"{sourceFileFullPath} does not exist");
            return false;
        }
    }
}
