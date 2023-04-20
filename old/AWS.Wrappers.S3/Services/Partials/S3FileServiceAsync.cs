using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service
{
    public async Task UploadFileAsync(S3Upload s3Upload, CancellationToken cancellationToken = default)
    {
        using (FileStream fsSource = new FileStream(s3Upload.SourcePath, FileMode.Open, FileAccess.Read))
        {
            s3Upload.DestPath = PathFormat(s3Upload.DestPath);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fsSource,
                Key = s3Upload.DestPath + s3Upload.DestFileName,
                BucketName = s3Upload.BucketName,
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest, cancellationToken);
        }
    }

    public async Task UploadFileAsync(S3UploadByBytes s3Upload, CancellationToken cancellationToken = default)
    {
        using (var fsSource = new MemoryStream(s3Upload.SourceFileBytes))
        {
            s3Upload.DestPath = PathFormat(s3Upload.DestPath);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fsSource,
                Key = s3Upload.DestPath + s3Upload.DestFileName,
                BucketName = s3Upload.BucketName,
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest, cancellationToken);
        }
    }

    public async Task DownloadFileToLocalAsync(S3Download s3Download, CancellationToken cancellationToken = default)
    {
        MemoryStream ms = null;
        GetObjectRequest getObjectRequest = new GetObjectRequest
        {
            BucketName = s3Download.BucketName,
            Key = s3Download.Key
        };

        using (var response = await _s3Client.GetObjectAsync(getObjectRequest, cancellationToken))
        {
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (ms = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(ms, cancellationToken);
                }
            }
        }

        await File.WriteAllBytesAsync(s3Download.LocalPath, ms?.ToArray(), cancellationToken);

    }

    public async Task<byte[]> GetFileBytesAsync(S3Download s3Download, CancellationToken cancellationToken = default)
    {
        MemoryStream ms = null;
        GetObjectRequest getObjectRequest = new GetObjectRequest
        {
            BucketName = s3Download.BucketName,
            Key = s3Download.Key
        };

        using (var response = await _s3Client.GetObjectAsync(getObjectRequest, cancellationToken))
        {
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                using (ms = new MemoryStream())
                {
                    await response.ResponseStream.CopyToAsync(ms, cancellationToken);
                }
            }
        }

        return ms?.ToArray();
    }

    public async Task<bool> IsFileExistsAsync(string bucketName, string s3Key, CancellationToken cancellationToken = default)
    {
        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
            Prefix = s3Key
        };

        var listResponse = await _s3Client.ListObjectsV2Async(request, cancellationToken);
        return listResponse.S3Objects.Any(o => o.Key == s3Key && o.BucketName == bucketName);
    }

    public async Task DeleteFileAsync(string bucketName, string s3Key, CancellationToken cancellationToken = default)
    {
        await _s3Client.DeleteObjectAsync(new Amazon.S3.Model.DeleteObjectRequest()
        {
            BucketName = bucketName,
            Key = s3Key
        }, cancellationToken);
    }


    public async Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        var request = new ListObjectsV2Request { BucketName = bucketName, Prefix = path, StartAfter = path };
        var lists = await _s3Client.ListObjectsV2Async(request, cancellationToken);

        var files = new List<string>();
        var file = "";
        foreach (var item in lists.S3Objects)
        {
            if (item.Size > 0)
            {
                file = item.Key.Replace(path + "/", "");
                if (!file.Contains('/'))
                {
                    files.Add(file);
                }
            }
        }

        return files;
    }

}
