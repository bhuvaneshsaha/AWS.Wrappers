
namespace AWS.S3.Wrapper.Tests;


public class DirectoryOperationsTests
{
    private readonly Mock<IAmazonS3> _amazonS3Mock;
    private readonly IDirectoryOperations _directoryOperations;

    public DirectoryOperationsTests()
    {
        _amazonS3Mock = new Mock<IAmazonS3>();
        _directoryOperations = new DirectoryOperations(_amazonS3Mock.Object);
    }
}
