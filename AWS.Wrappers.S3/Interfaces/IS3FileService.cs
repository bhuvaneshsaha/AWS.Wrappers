using AWS.Wrappers.S3.Models;

namespace AWS.Wrappers.S3.Interfaces;

public interface IS3FileService
{
     /// <summary>
    /// Upload local file to S3 path
    /// </summary>
    /// <param name="s3Upload"></param>
    /// <returns>Success (True) or False</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="IOException"></exception>
    void UploadFile(S3Upload s3Upload);

    /// <summary>
    /// Upload file bytes to S3 path
    /// </summary>
    /// <param name="s3Uploads"></param>
    /// <returns></returns>
    void UploadFile(S3UploadByBytes s3Uploads);
    
    /// <summary>
    /// Upload list of local files to given S3 path
    /// </summary>
    /// <param name="s3Uploads"></param>
    /// <returns></returns>
    void BulkUpload(S3Upload[] s3Uploads);
    void BulkUpload(S3UploadByBytes[] s3Uploads);

    void DownloadFileToLocal(S3Download s3Download);
    void BulkDownloadToLocal(S3Download[] s3Download);

    byte[] GetFileBytes(S3Download s3Download);
    
    bool IsFileExists(string s3Key, string bucketName);
    void DeleteFile(string s3Key, string bucketName);

    List<string> GetFiles(string bucketName, string path);
    Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default);
}
