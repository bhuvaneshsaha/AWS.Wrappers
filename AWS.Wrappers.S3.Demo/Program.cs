using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;

S3ServiceLocal S3Service = new S3ServiceLocal();

Console.WriteLine("Hello, World!");

public class S3ServiceLocal
{
    //private IS3Service? _S3Service;

    public S3ServiceLocal()
    {
       IS3Service? _S3Service = new S3Service("<AccessKey>", "<SecretKey>", S3Regions.USEast1);
       
    }  
}
