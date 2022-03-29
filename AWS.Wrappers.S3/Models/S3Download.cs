namespace AWS.Wrappers.S3.Models;

public class S3Download
{
    public string BucketName { get; set; }
    public string Key { get; set; }
    public string LocalPath { get; set; }
}
