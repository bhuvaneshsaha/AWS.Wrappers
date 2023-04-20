namespace AWS.Wrappers.S3.Models;

public class S3Download
{
    public string BucketName { get; set; }
    public string Key { get; set; }

    /// <summary>
    /// Needed for DownloadFileToLocal/DownloadFileToLocalAsync methods to store file in local.
    /// </summary>
    public string LocalPath { get; set; }
}
