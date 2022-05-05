using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service : IS3Service
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

    #endregion

}
