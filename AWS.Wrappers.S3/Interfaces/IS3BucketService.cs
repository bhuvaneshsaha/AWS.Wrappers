namespace AWS.Wrappers.S3.Interfaces;

public interface IS3BucketService
{
    /// <summary>
    /// It creates a bucket with the given name in the same region where the S3 object was created.
    /// Bucket name should be unique across global.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    Task CreateBucketAsync(string bucketName);

    /// <summary>
    /// It creates a bucket with the given name in the same region where the S3 object was created.
    /// Bucket name should be unique across global.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    void CreateBucket(string bucketName);

    /// <summary>
    /// Deletes the bucket if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    Task DeleteBucketAsync(string bucketName);

    /// <summary>
    /// Deletes the bucket if exists
    /// </summary>
    /// <param name="bucketName"></param>
    /// <exception cref="ArgumentException"/>
    void DeleteBucket(string bucketName);

    /// <summary>
    /// Check if bucket exists in S3 or not.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    Task<bool> IsBucketExistsAsync(string bucketName);

    /// <summary>
    /// Check if bucket exists in S3 or not.
    /// </summary>
    /// <param name="bucketName"></param>
    /// <returns>Exists (True) / Not exists (False)</returns>
    bool IsBucketExists(string bucketName);
    
}

