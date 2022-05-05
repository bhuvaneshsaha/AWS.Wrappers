using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service
{
    public async Task<bool> IsDirectoryExistsAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        path = PathFormat(path);

        ListObjectsV2Request request = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path
        };

        var response = await _s3Client.ListObjectsV2Async(request, cancellationToken);
        return (response != null && response.S3Objects != null && response.S3Objects.Count > 0);
    }

    public async Task CreateDirectoryAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        path = PathFormat(path);

        if (await IsDirectoryExistsAsync(bucketName, path)) return;

        var request = new PutObjectRequest()
        {
            BucketName = bucketName,
            StorageClass = S3StorageClass.Standard,
            ServerSideEncryptionMethod = ServerSideEncryptionMethod.None,
            Key = path,
            ContentBody = string.Empty
        };

        var response = await _s3Client.PutObjectAsync(request, cancellationToken);

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }
    }

    public async Task DeleteDirectoryAsync(string bucketName, string path, bool recursive = false, CancellationToken cancellationToken = default)
    {
        path = PathFormat(path);

        if (!await IsDirectoryExistsAsync(bucketName, path, cancellationToken))
        {
            return;
        }

        var subFolders = await GetSubDirectoriesAsync(bucketName, path, cancellationToken);
        //Todo: Need to modify after implementing GetFiles
        //var files = GetFiles(bucketName, path);

        //if (!recursive && subFolders?.Count > 0 || files?.Count > 0)
        if (!recursive && subFolders?.Count > 0)
        {
            throw new InvalidOperationException("The directory is not empty. Try recrusive = true to delete recrusively.");
        }

        DeleteObjectRequest deleteObject = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = path,
        };

        var response = await _s3Client.DeleteObjectAsync(deleteObject, cancellationToken);

        if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent || response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }
    }

    public async Task<List<string>> GetSubDirectoriesAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        path = PathFormat(path);

        ListObjectsV2Request listObjectsV2 = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path,
            StartAfter = path
        };

        var response = await _s3Client.ListObjectsV2Async(listObjectsV2, cancellationToken);

        var files = new List<string>();

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }

        var temp = "";
        foreach (var item in response.S3Objects)
        {
            temp = item.Key.Replace(path, "");
            if (item.Size == 0)
            {
                temp = temp.Split('/')[0];
                if (!files.Contains(temp))
                {
                    files.Add(temp);
                }
            }
        }

        return files;
    }

    public async Task<List<string>> GetAllDirectoriesRecursiveAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        path = PathFormat(path);

        ListObjectsV2Request listObjectsV2 = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path,
            StartAfter = path
        };

        var response = await _s3Client.ListObjectsV2Async(listObjectsV2, cancellationToken);

        var files = new List<string>();

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }

        foreach (var item in response.S3Objects)
        {
            files.Add(item.Key);
        }

        return files;
    }


}
