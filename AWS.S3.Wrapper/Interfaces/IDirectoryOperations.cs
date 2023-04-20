using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Interfaces
{
    public interface IDirectoryOperations
    {
        void CreateDirectory(string bucketName, string directoryPath);
        IEnumerable<string> ListDirectories(string bucketName, string parentDirectoryPath = "");
        void DeleteDirectory(string bucketName, string directoryPath);

        Task CreateDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> ListDirectoriesAsync(string bucketName, string parentDirectoryPath = "", CancellationToken cancellationToken = default);
        Task DeleteDirectoryAsync(string bucketName, string directoryPath, CancellationToken cancellationToken = default);
    }
}