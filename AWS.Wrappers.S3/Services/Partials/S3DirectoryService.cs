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
    public bool IsDirectoryExists(string bucketName, string path)
    {
        path = PathFormat(path);

        ListObjectsV2Request request = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path
        };

        var response = _s3Client.ListObjectsV2Async(request).GetAwaiter().GetResult();
        return (response != null && response.S3Objects != null && response.S3Objects.Count > 0);
    }

    public void CreateDirectory(string bucketName, string path)
    {
        path = PathFormat(path);

        if (IsDirectoryExists(bucketName, path)) return;

        var request = new PutObjectRequest()
        {
            BucketName = bucketName,
            StorageClass = S3StorageClass.Standard,
            ServerSideEncryptionMethod = ServerSideEncryptionMethod.None,
            Key = path,
            ContentBody = string.Empty
        };

        var response = _s3Client.PutObjectAsync(request).GetAwaiter().GetResult();

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }
    }

    public void DeleteDirectory(string bucketName, string path, bool recursive = false)
    {
        path = PathFormat(path);

        if (!IsDirectoryExists(bucketName, path))
        {
            return;
        }

        var subFolders = GetSubDirectories(bucketName, path);
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

        var response = _s3Client.DeleteObjectAsync(deleteObject).GetAwaiter().GetResult();

        if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent || response.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(response.ResponseMetadata.ToString());
        }
    }

    public List<string> GetSubDirectories(string bucketName, string path)
    {
        path = PathFormat(path);

        ListObjectsV2Request listObjectsV2 = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path,
            StartAfter = path
        };

        var response = _s3Client.ListObjectsV2Async(listObjectsV2).GetAwaiter().GetResult();

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
                if (files.Contains(temp))
                {
                    files.Add(temp);
                }
            }
        }

        return files;
    }

    public List<string> GetAllDirectoriesRecursive(string bucketName, string path)
    {
        path = PathFormat(path);

        ListObjectsV2Request listObjectsV2 = new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = path,
            StartAfter = path
        };

        var response = _s3Client.ListObjectsV2Async(listObjectsV2).GetAwaiter().GetResult();

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
