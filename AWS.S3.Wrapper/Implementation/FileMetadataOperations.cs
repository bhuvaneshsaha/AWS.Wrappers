using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;

namespace AWS.S3.Wrapper.Implementation;

public class FileMetadataOperations : IFileMetadataOperations
{
    private readonly IAmazonS3 _s3Client;

    public FileMetadataOperations(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<IDictionary<string, string>> GetObjectMetadataAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
    {
        var request = new Amazon.S3.Model.GetObjectMetadataRequest
        {
            BucketName = bucketName,
            Key = objectKey
        };

        var response = await _s3Client.GetObjectMetadataAsync(request, cancellationToken);
        MetadataCollection metadataCollection = response.Metadata;

        return metadataCollection.Keys.ToDictionary(key => key, key => metadataCollection[key]);
    }

    public async Task UpdateObjectMetadataAsync(string bucketName, string objectKey, IDictionary<string, string> metadata, CancellationToken cancellationToken)
    {
        var copyRequest = new Amazon.S3.Model.CopyObjectRequest
        {
            SourceBucket = bucketName,
            SourceKey = objectKey,
            DestinationBucket = bucketName,
            DestinationKey = objectKey,
            MetadataDirective = S3MetadataDirective.REPLACE
        };

        foreach (var entry in metadata)
        {
            copyRequest.Metadata[entry.Key] = entry.Value;
            copyRequest.Metadata[entry.Key] = copyRequest.Metadata[entry.Key].Replace("x-amz-meta-", "");
        }

        await _s3Client.CopyObjectAsync(copyRequest, cancellationToken);
    }

    private string RemovePrefix(string key)
{
    const string prefix = "x-amz-meta-";
    return key.StartsWith(prefix) ? key.Substring(prefix.Length) : key;
}
}
