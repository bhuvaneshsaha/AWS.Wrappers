using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;

S3ServiceLocal S3Service = new S3ServiceLocal();
await S3Service.Test();
//S3Service.Test2();
Console.WriteLine("Hello, World!");

public class S3ServiceLocal
{
    //private IS3Service? _S3Service;

    public S3ServiceLocal()
    {
       
    }  

    public async Task Test()
    {
        var accessKey = "<AccessKey>";
        var secretKey = "<SecretKey>";
        IS3Service? s3Service = new S3Service(accessKey, secretKey, S3Regions.USEast1);

        var bucketName = "tet-budf-fdafsa3424-fsfa";
        var path1 = "Folder2/A";
        var paths = await s3Service.GetSubDirectoriesAsync(bucketName, path1);


    }

    public void Test2()
    {
        var accessKey = "AKIARPSFPGN4TGNBJUY3";
        var secretKey = "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";
        IS3Service? s3Service = new S3Service(accessKey, secretKey, S3Regions.USEast1);

        var bucketName = "tet-budf-fdafsa3424-fsfa";
        var path1 = "Folder2";
        var paths = s3Service.GetSubDirectories(bucketName, path1);


    }

}
