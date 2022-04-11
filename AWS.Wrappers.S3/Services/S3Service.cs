using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public class S3Service : IS3Service
{

    private readonly IAmazonS3 _s3Client;
    private readonly string _s3Region;
    #region Constructors

    /// <summary>
    /// Creates S3 Client using Access Key and Secret Key to access S3 resources
    /// </summary>
    /// <param name="accessKey"></param>
    /// <param name="secretKey"></param>
    /// <param name="region"></param>
    /// <exception cref="ArgumentException"></exception>
    public S3Service(string accessKey, string secretKey, string region)
    {
        ValidateKeyConstructor(accessKey, secretKey);

        var endpoint = RegionEndpoint.GetBySystemName(region);

        _s3Region = region;

        if (endpoint == null) throw new ArgumentException("Invalid Region");

        _s3Client = new AmazonS3Client(accessKey, secretKey, endpoint);
    }

    #endregion

    #region Public Methods
    public void BulkDownloadToLocal(S3Download[] s3Download)
    {
        try
        {
            
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public void BulkUpload(S3Upload[] s3Uploads)
    {
        throw new NotImplementedException();
    }

    public void BulkUpload(S3UploadByBytes[] s3Uploads)
    {
        throw new NotImplementedException();
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            var respone = await _s3Client.PutBucketAsync(bucketName, cancellationToken);

            if (respone.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(respone.ResponseMetadata.ToString());
            }
        }
        catch
        {
            throw;
        }
    }

    public void CreateBucket(string bucketName)
    {
        try
        {
            var response = _s3Client.PutBucketAsync(bucketName).GetAwaiter().GetResult();

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(response.ResponseMetadata.ToString());
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task CreateDirectoryAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public void CreateDirectory(string bucketName, string path)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            if (await IsBucketExistsAsync(bucketName))
            {
                var response = await _s3Client.DeleteBucketAsync(bucketName, cancellationToken);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new ArgumentException(response.ResponseMetadata.ToString());
                }
                return;
            }

            throw new ArgumentException("Bucket not found");
        }
        catch
        {
            throw;
        }
    }

    public void DeleteBucket(string bucketName)
    {
        try
        {
            if (IsBucketExists(bucketName))
            {
                var response = _s3Client.DeleteBucketAsync(bucketName).GetAwaiter().GetResult();

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new ArgumentException(response.ResponseMetadata.ToString());
                }
                return;
            }

            throw new ArgumentException("Bucket not found");
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteDirectoryAsync(string bucketName, string path, bool recursive = false, CancellationToken cancellationToken = default)
    {
        try
        {
            path = PathFormat(path);

            if (! await IsDirectoryExistsAsync(bucketName, path, cancellationToken))
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
        catch
        {
            throw;
        }
    }

    public void DeleteDirectory(string bucketName, string path, bool recursive = false)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public void DeleteFile(string s3Key, string bucketName)
    {
        throw new NotImplementedException();
    }

    public void DownloadFileToLocal(S3Download s3Download)
    {
        throw new NotImplementedException();
    }

    public byte[] GetFileBytes(S3Download s3Download)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetAllDirectoriesRecursiveAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public List<string> GetAllDirectoriesRecursive(string bucketName, string path)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public async Task<List<string>> GetSubDirectoriesAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public List<string> GetSubDirectories(string bucketName, string path)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public async Task<bool> IsBucketExistsAsync(string bucketName)
    {
        try
        {
            return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        }
        catch
        {
            throw;
        }
    }

    public bool IsBucketExists(string bucketName)
    {
        try
        {
            return AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName).GetAwaiter().GetResult();
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> IsDirectoryExistsAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public bool IsDirectoryExists(string bucketName, string path)
    {
        try
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
        catch
        {
            throw;
        }
    }

    public bool IsFileExists(string s3Key, string bucketName)
    {
        throw new NotImplementedException();
    }

    public void UploadFile(S3Upload s3Upload)
    {
        throw new NotImplementedException();
    }

    public void UploadFile(S3UploadByBytes s3Uploads)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Private Methods

    private void ValidateKeyConstructor(string accessKey, string secretKey)
    {
        if (string.IsNullOrEmpty(accessKey)) throw new ArgumentException("Access Key Missing");
        if (string.IsNullOrEmpty(secretKey)) throw new ArgumentException("Secret Key Missing");
    }

    private S3Region GetRegion(string region)
    {
        return new S3Region(region);
    }

    private string PathFormat(string path)
    {
        path = path.Replace("\\", "/");
        path = path.EndsWith("/") ? path : path +"/";
        return path;
    }

    public List<string> GetFiles(string bucketName, string path)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

}
