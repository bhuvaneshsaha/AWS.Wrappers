using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Interfaces
{
    public interface IS3FileServiceAsync
    {
        Task<List<string>> GetFilesAsync(string bucketName, string path, CancellationToken cancellationToken = default);
    }
}
