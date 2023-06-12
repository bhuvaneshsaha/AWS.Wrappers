
namespace AWS.S3.Wrapper.Implementation;

public class DirectoryOperations : IDirectoryOperations
{
    private readonly IAmazonS3 _s3Client;
    public DirectoryOperations(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;

    }
    public void CreateDirectory(string bucketName, string directoryPath)
    {
        if (!directoryPath.EndsWith("/"))
        {
            directoryPath += "/";
        }

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = directoryPath,
            InputStream = new MemoryStream()
        };

        _s3Client.PutObjectAsync(putObjectRequest).Wait();

    }

    public async Task CreateDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default)
    {
        if (!directoryPath.EndsWith("/"))
        {
            directoryPath += "/";
        }

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = directoryPath,
            InputStream = new MemoryStream()
        };

        await _s3Client.PutObjectAsync(putObjectRequest, cancellationToken);
    }

    public void DeleteDirectory(string bucketName, string directoryPath)
    {
        if (!directoryPath.EndsWith("/"))
        {
            directoryPath += "/";
        }

        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = directoryPath
        };

        ListObjectsV2Response listObjectsResponse;
        do
        {
            // List all objects under the directory
            listObjectsResponse = _s3Client.ListObjectsV2Async(listObjectsRequest).Result;
            if (listObjectsResponse.S3Objects.Count > 0)
            {
                // Delete all objects under the directory
                var deleteObjectsRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = listObjectsResponse.S3Objects.Select(o => new KeyVersion { Key = o.Key }).ToList()
                };

                _s3Client.DeleteObjectsAsync(deleteObjectsRequest).Wait();
            }

            // If response is truncated, set the marker to get the next batch of objects
            if (listObjectsResponse.IsTruncated)
            {
                listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
            }

        } while (listObjectsResponse.IsTruncated); // Continue while there are more objects to list
    }

    public async Task DeleteDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default)
    {
        if (!directoryPath.EndsWith("/"))
        {
            directoryPath += "/";
        }

        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = directoryPath
        };

        ListObjectsV2Response listObjectsResponse;
        do
        {
            // List all objects under the directory
            listObjectsResponse = await _s3Client.ListObjectsV2Async(listObjectsRequest, cancellationToken);
            if (listObjectsResponse.S3Objects.Count > 0)
            {
                // Delete all objects under the directory
                var deleteObjectsRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = listObjectsResponse.S3Objects.Select(o => new KeyVersion { Key = o.Key }).ToList()
                };

                await _s3Client.DeleteObjectsAsync(deleteObjectsRequest, cancellationToken);
            }

            // If response is truncated, set the marker to get the next batch of objects
            if (listObjectsResponse.IsTruncated)
            {
                listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
            }

        } while (listObjectsResponse.IsTruncated); // Continue while there are more objects to list
    }

    public async Task<IEnumerable<string>> ListDirectoriesAsync(string bucketName, string parentDirectoryPath = "", CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(parentDirectoryPath) && !parentDirectoryPath.EndsWith("/"))
        {
            parentDirectoryPath += "/";
        }

        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = parentDirectoryPath,
            Delimiter = "/"
        };

        var directories = new List<string>();
        ListObjectsV2Response listObjectsResponse;
        do
        {
            // List objects and common prefixes under the directory
            listObjectsResponse = await _s3Client.ListObjectsV2Async(listObjectsRequest, cancellationToken);

            // Add the common prefixes to the directories list
            directories.AddRange(listObjectsResponse.CommonPrefixes);

            // If response is truncated, set the marker to get the next batch of objects
            if (listObjectsResponse.IsTruncated)
            {
                listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
            }

        } while (listObjectsResponse.IsTruncated); // Continue while there are more objects to list

        return directories;
    }

    public IEnumerable<string> ListDirectories(string bucketName, string parentDirectoryPath = "")
    {
        if (!string.IsNullOrEmpty(parentDirectoryPath) && !parentDirectoryPath.EndsWith("/"))
        {
            parentDirectoryPath += "/";
        }

        var listObjectsRequest = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = parentDirectoryPath,
            Delimiter = "/"
        };

        var directories = new List<string>();
        ListObjectsV2Response listObjectsResponse;
        do
        {
            // List objects and common prefixes under the directory
            listObjectsResponse = _s3Client.ListObjectsV2Async(listObjectsRequest).Result;

            // Add the common prefixes to the directories list
            directories.AddRange(listObjectsResponse.CommonPrefixes);

            // If response is truncated, set the marker to get the next batch of objects
            if (listObjectsResponse.IsTruncated)
            {
                listObjectsRequest.ContinuationToken = listObjectsResponse.NextContinuationToken;
            }

        } while (listObjectsResponse.IsTruncated); // Continue while there are more objects to list

        return directories;
    }

}
