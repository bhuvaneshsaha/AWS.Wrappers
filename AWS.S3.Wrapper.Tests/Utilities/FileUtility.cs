

namespace AWS.S3.Wrapper.Tests.Utilities;
public static class FileUtility
{
    public static async Task CreateFileInLocal(List<string> fileNames, string? localPath)
    {
        localPath ??= Directory.GetCurrentDirectory();
        foreach (var fileName in fileNames)
        {
            var fullPath = Path.Combine(localPath, fileName);
            if (!File.Exists(fullPath))
            {
                await File.WriteAllTextAsync(fullPath, $"Hello World {fileNames.IndexOf(fileName) + 1}!");
            }
        }
    }
    public static void GenerateLargeFile(string filePath, long fileSizeInBytes)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        fileStream.SetLength(fileSizeInBytes);
    }

    public static void DeleteFilesInDirectory(string directoryPath)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        foreach (var file in directoryInfo.GetFiles())
        {
            file.Delete();
        }
    }

    public static string GetLocalTestFilesPath()
    {
        var localPath = Directory.GetCurrentDirectory() + "/TestFiles";

        if(!Directory.Exists(localPath))
            Directory.CreateDirectory(localPath);

        return localPath;
    }

    public static string GetLocalTestFilesPathWithFolder(string folderName)
    {
        var localPath = Directory.GetCurrentDirectory() + "/TestFiles/" + folderName;

        if(!Directory.Exists(localPath))
            Directory.CreateDirectory(localPath);

        return localPath;
    }
}
