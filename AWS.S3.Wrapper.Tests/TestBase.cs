using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.S3.Wrapper.Tests;
public class TestBase
{
    protected static string AWS_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
    protected static string AWS_ACCESS_SECRET = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
}
