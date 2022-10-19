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
    void UploadFile(S3UploadByBytes s3Upload);

    /// <summary>
    /// Download the S3 file and save it in local path
    /// </summary>
    /// <param name="s3Download"></param>
    void DownloadFileToLocal(S3Download s3Download);

    /// <summary>
    /// Get Bytes of File from S3
    /// </summary>
    /// <param name="s3Download"></param>
    /// <returns></returns>
    byte[] GetFileBytes(S3Download s3Download);

    /// <summary>
    /// ​Checks file object exist in S3​
    /// </summary>
    /// <param name="s3Key"></param>
    /// <param name="bucketName"></param>
    /// <returns></returns>
    bool IsFileExists(string bucketName, string s3Key);

    /// <summary>
    /// Delete S3 file if exists
    /// </summary>
    /// <param name="s3Key"></param>
    /// <param name="bucketName"></param>
    void DeleteFile(string bucketName, string s3Key);

    /// <summary>
    /// Get list of files from path
    /// </summary>
    /// <param name="bucketName"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    List<string> GetFiles(string bucketName, string path);

}
