
# AWS S3 Wrappers

[![CI Pipeline](https://github.com/bhuvaneshsaha/AWS.Wrappers/actions/workflows/ci.yaml/badge.svg)](https://github.com/bhuvaneshsaha/AWS.Wrappers/actions/workflows/ci.yaml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bhuvaneshsaha&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bhuvaneshsaha)
[![CodeFactor](https://www.codefactor.io/repository/github/bhuvaneshsaha/aws.wrappers/badge)](https://www.codefactor.io/repository/github/bhuvaneshsaha/aws.wrappers)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/14753260313949559c4c9012cb70bc97)](https://app.codacy.com/gh/bhuvaneshsaha/AWS.Wrappers/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/14753260313949559c4c9012cb70bc97)](https://app.codacy.com/gh/bhuvaneshsaha/AWS.Wrappers/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage)
[![codecov](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers/branch/master/graph/badge.svg?token=CL7JXQF2MI)](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers)
[![Open Source Helpers](https://www.codetriage.com/bhuvaneshsaha/aws.wrappers/badges/users.svg)](https://www.codetriage.com/bhuvaneshsaha/aws.wrappers)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fbhuvaneshsaha%2FAWS.Wrappers.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fbhuvaneshsaha%2FAWS.Wrappers?ref=badge_shield)

![Image Caption](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers/branch/master/graphs/icicle.svg?token=CL7JXQF2MI)

AWS Wrappers is a straightforward **.NET 8 C# class library** to perform basic operations AWS S3 from the .NET core application which is built on top of AWS libraries, which Amazon Web Services provide.

The AWS libraries provide all the features needed to perform operations with AWS, but they are sometimes not straight forward to use and we need to implement them in our application.

So, AWS Wrappers S3 is designed to do basic operations such as creating buckets, uploading files, creating folders, etc. with the AWS Simple Storage Services (S3) with simple, user-readable methods that already implement the codes required to perform the operation.

### Prerequisites
 - .NET 8
 - AWSSDK.S3 [Version 3.7.304.10]

## Usage
Clone the code from the master branch or download stable release from the release area.

### Create an S3 client

```C#
var s3Client = new AmazonS3Client();
```
> Refer: [How to create AmazonS3Client](docs/AWS_CLIENT.md)

### Create an instance of the BucketOperations class

```C#
IBucketOperations bucketOperations = new BucketOperations(s3Client);
```

### Create a bucket

```C#
await bucketOperations.CreateBucketAsync("my-bucket");
```

> **Note**: While this wrapper provides a familiar way to interact with Amazon S3, it's important to remember that S3 doesn't have the same concept of files and directories like a traditional file system. Instead, it functions based on key-value pairs. In S3, the key is used to represent the file path or object name, while the value corresponds to the actual file content. However, you can structure the key to resemble a directory path, allowing for a hierarchical organization of objects within the S3 bucket.

Please find the available methods below

 - [Bucket Operations](/docs/BUCKET_OPERTATIONS.md)

 - [Directory Operations](/docs/DIRECTORY_OPERATIONS.md)

 - [File Operations](/docs/FILE_OPERATIONS.md)

 - [File Metadata Operations](/docs/FILE_METADATA_OPERATIONS.md)

 - [Large File Operations](/docs/LARGE_FILE_OPERATIONS.md)

 - [Presigned URL Operations](/docs/PRESIGNED_URL_OPERATIONS.md)


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fbhuvaneshsaha%2FAWS.Wrappers.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fbhuvaneshsaha%2FAWS.Wrappers?ref=badge_large)
