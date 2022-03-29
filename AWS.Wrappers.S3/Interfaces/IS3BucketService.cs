namespace AWS.Wrappers.S3.Interfaces;

public interface IS3BucketService
{
    /// <summary>
    /// Creates bucket in the given name if already not exists.
    /// Bucket name should be unique across global.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    Task CreateBucketAsync(string bucketName, string region = "us-east-1");

    /// <summary>
    /// Creates bucket in the given name if already not exists.
    /// Bucket name should be unique across global.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    void CreateBucket(string bucketName, string region = "us-east-1");

    /// <summary>
    /// Deletes the bucket if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    void DeleteBucket(string bucketName);

    /// <summary>
    /// Check for bucket exists in S3 or not
    /// </summary>
    /// <param name="bucketName"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    bool IsBucketExists(string bucketName);
}

