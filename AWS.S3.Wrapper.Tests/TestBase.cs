using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests;
public class TestBase
{
    protected readonly IAmazonS3 client;
    protected static string AWS_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
    protected static string AWS_ACCESS_SECRET = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");

    public TestBase()
    {
        client = new AmazonS3Client(AWS_ACCESS_KEY, AWS_ACCESS_SECRET, Amazon.RegionEndpoint.USEast1);
    }
}
