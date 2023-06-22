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
        System.Console.WriteLine(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"));
        System.Console.WriteLine(Environment.GetEnvironmentVariable("{{ secrets.AWS_ACCESS_KEY_ID }}"));
    }
}
