
# AWS Wrappers S3 (AWSWS3)


AWS Wrappers is a straight-forward **.NET 6 C# class library** that is built on top of AWS libraries, which are provided by Amazon Web Services.

The AWS libraries provide all the features needed to perform operations with AWS, but they are sometimes not straight forward to use and we need to implement them in our application.

So, AWS Wrappers S3 is designed to do basic operations such as creating buckets, uploading files, creating folders, etc. with the AWS Simple Storage Services (S3) with simple, user-readable methods that already implement the codes required to perform the operation.

## Usage

Users can copy the `AWS.Wrappers.S3` class library into their project solution folder and add references (dependencies) to their project, so they can utilize the library.

If a user prefers their project to have an S3 Wrapper (to have their own project name), they can simply copy the necessary interfaces and classes into their projects.

> The library contains both synchronous and asynchronous methods.

Package which need to be imported,
```
using AWS.Wrappers.S3.Interfaces;
using AWS.Wrappers.S3.Services;
```

Creating S3Service Object,

```
IS3Service? _S3Service = new S3Service("<AccessKey>", "<SecretKey>", S3Regions.USEast1);
```

Sample code to create bucket in S3,

_In synchronous,_
```
    IS3Service S3Service = new S3Service("<AccessKey>", "<SecretKey>", S3Regions.USEast1);
    S3Service.CreateBucket("<new-bucket-name>");
```

_In asynchronous,_
```
    IS3Service S3Service = new S3Service("<AccessKey>", "<SecretKey>", S3Regions.USEast1);
    await S3Service.CreateBucketAsync("<new-bucket-name>");
```

> Always use code from the master branch or download code from the release area.
You can find the version information below,
>>[Version 1.0.0](AWS.Wrappers.S3\Versions\V1_0_0.md)