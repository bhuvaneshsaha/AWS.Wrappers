using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
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

        if (endpoint == null) throw new ArgumentException("Invalid Region");

        _s3Client = new AmazonS3Client(accessKey, secretKey, endpoint);
    }


    #endregion

    #region Public Methods
    public void BulkDownloadToLocal(S3Download[] s3Download)
    {
        throw new NotImplementedException();
    }

    public void BulkUpload(S3Upload[] s3Uploads)
    {
        throw new NotImplementedException();
    }

    public void BulkUpload(S3UploadByBytes[] s3Uploads)
    {
        throw new NotImplementedException();
    }

    public async Task CreateBucketAsync(string bucketName, string region = "us-east-1")
    {
        var bucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            BucketRegion = region
        };
        var respone = await _s3Client.PutBucketAsync(bucketRequest);

        if (respone.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(respone.ResponseMetadata.ToString());
        }
    }

    public void CreateBucket(string bucketName, string region = "us-east-1")
    {
        var bucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            BucketRegion = region,
        };
        var respone = _s3Client.PutBucketAsync(bucketRequest).GetAwaiter().GetResult();

        if (respone.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new ArgumentException(respone.ResponseMetadata.ToString());
        }
    }

    public void CreateDirectory(string bucketName, string folderName)
    {
        throw new NotImplementedException();
    }

    public void DeleteBucket(string bucketName)
    {
        throw new NotImplementedException();
    }

    public bool DeleteDirectory(string bucketName, string folderName, bool recursive = false)
    {
        throw new NotImplementedException();
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

    public bool IsBucketExists(string bucketName)
    {
        throw new NotImplementedException();
    }

    public bool IsDirectoryExists(string bucketName, string folderName)
    {
        throw new NotImplementedException();
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


    #endregion

}
