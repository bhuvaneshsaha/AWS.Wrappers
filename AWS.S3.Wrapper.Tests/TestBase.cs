using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests;
public class TestBase
{
    protected static readonly IAmazonS3 client = new AmazonS3Client(
        Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? Environment.GetEnvironmentVariable("{{ secrets.AWS_ACCESS_KEY_ID }}"),
        Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? Environment.GetEnvironmentVariable("{{ secrets.AWS_SECRET_ACCESS_KEY }}"),
        Amazon.RegionEndpoint.USEast1
    );

    public TestBase()
    {
        string accessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        // string secretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

        Console.WriteLine("Access key ID: " + accessKeyId);
    }
}
