using Amazon.S3;
using Amazon.S3.Transfer;
using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Services;

public partial class S3Service
{
    public void UploadFile(S3UploadByBytes s3Uploads)
    {
        throw new NotImplementedException();
    }

    public void UploadFile(S3Upload s3Upload)
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
            fileTransferUtility.UploadAsync(uploadRequest).GetAwaiter().GetResult();
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

    public void BulkDownloadToLocal(S3Download[] s3Download)
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

    public bool IsFileExists(string s3Key, string bucketName)
    {
        throw new NotImplementedException();
    }

    public List<string> GetFiles(string bucketName, string path)
    {
        throw new NotImplementedException();
    }

}
