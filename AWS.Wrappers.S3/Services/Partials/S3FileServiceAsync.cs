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

    public Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}
