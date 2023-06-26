
# AWS S3 Wrappers

[![.NET](https://github.com/bhuvaneshsaha/AWS.Wrappers/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bhuvaneshsaha/AWS.Wrappers/actions/workflows/dotnet.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/bhuvaneshsaha/aws.wrappers/badge)](https://www.codefactor.io/repository/github/bhuvaneshsaha/aws.wrappers)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/14753260313949559c4c9012cb70bc97)](https://app.codacy.com/gh/bhuvaneshsaha/AWS.Wrappers/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/14753260313949559c4c9012cb70bc97)](https://app.codacy.com/gh/bhuvaneshsaha/AWS.Wrappers/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage)
[![codecov](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers/branch/master/graph/badge.svg?token=CL7JXQF2MI)](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers)

![Image Caption](https://codecov.io/gh/bhuvaneshsaha/AWS.Wrappers/branch/master/graphs/icicle.svg?token=CL7JXQF2MI)

AWS Wrappers is a straightforward **.NET 6 C# class library** to perform basic operations AWS S3 from the .NET core application which is built on top of AWS libraries, which Amazon Web Services provide.

The AWS libraries provide all the features needed to perform operations with AWS, but they are sometimes not straight forward to use and we need to implement them in our application.

So, AWS Wrappers S3 is designed to do basic operations such as creating buckets, uploading files, creating folders, etc. with the AWS Simple Storage Services (S3) with simple, user-readable methods that already implement the codes required to perform the operation.

### Prerequisites
 - .NET 7
 - AWSSDK.S3 [Version 3.7.104.30]

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
## Bucket Operations
| Method Name          | Description                                      | How to Use                                                                   | Example                                                                                              |   |
|----------------------|--------------------------------------------------|------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|---|
| CreateBucketAsync    | Creates a new S3 bucket with the specified name. | CreateBucketAsync(string bucketName, CancellationToken cancellationToken)    | await bucketOperations.CreateBucketAsync("my-bucket-name", cancellationToken);                       |   |
| DeleteBucketAsync    | Deletes the specified S3 bucket.                 | DeleteBucketAsync(string bucketName, CancellationToken cancellationToken)    | await bucketOperations.DeleteBucketAsync("my-bucket-name", cancellationToken);                       |   |
| DoesBucketExistAsync | Checks if the specified S3 bucket exists.        | DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken) | var bucketExists = await bucketOperations.DoesBucketExistAsync("my-bucket-name", cancellationToken); |   |
| ListBucketsAsync     | Lists all S3 buckets in the account.             | ListBucketsAsync(CancellationToken cancellationToken)                        | var buckets = await bucketOperations.ListBucketsAsync(cancellationToken);                            |   |
| EmptyBucketAsync     | Deletes all objects in the specified S3 bucket.  | EmptyBucketAsync(string bucketName, CancellationToken cancellationToken)     | await bucketOperations.EmptyBucketAsync("my-bucket-name", cancellationToken);                        |   |

## Directory Operations
| Method Name          | Description                                                  | How to Use                                                                                                                    | Example                                                                                                                      |
|----------------------|--------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|
| CreateDirectoryAsync | Creates a directory in an S3 bucket.                         | Provide the bucket name, directory path, and a cancellation token.                                                            | await directoryOperations.CreateDirectoryAsync("bucket-name", "directory-path", cancellationToken);                          |
| DeleteDirectoryAsync | Deletes a directory and all its contents from an S3 bucket.  | Provide the bucket name, directory path, and a cancellation token.                                                            | await directoryOperations.DeleteDirectoryAsync("bucket-name", "directory-path", cancellationToken);                          |
| ListDirectoriesAsync | Lists all directories in a parent directory in an S3 bucket. | Provide the bucket name, parent directory path, and a cancellation token. Returns an enumerable of directory paths.           | var directories = await directoryOperations.ListDirectoriesAsync("bucket-name", "parent-directory-path", cancellationToken); |
| EmptyDirectoryAsync  | Deletes all objects in a directory in an S3 bucket.          | Provide the bucket name, directory path, and a cancellation token.                                                            | await directoryOperations.EmptyDirectoryAsync("bucket-name", "directory-path", cancellationToken);                           |
| DirectoryExistsAsync | Checks if a directory exists in an S3 bucket.                | Provide the bucket name, directory path, and a cancellation token. Returns a boolean indicating whether the directory exists. | var directoryExists = await directoryOperations.DirectoryExistsAsync("bucket-name", "directory-path", cancellationToken);    |

## File Operations
| Method Name           | Description                                      | How to Use                                                                                                                                                   | Example                                                                                                                                            |   |
|-----------------------|--------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|---|
| CopyFileAsync         | Copies a file from one S3 bucket to another.     | Provide the source bucket name, source object key, destination bucket name, destination object key, and a cancellation token.                                | await fileOperations.CopyFileAsync("source-bucket", "source-object-key", "destination-bucket", "destination-object-key", cancellationToken);       |   |
| DeleteFileAsync       | Deletes a file from an S3 bucket.                | Provide the bucket name, object key, and a cancellation token.                                                                                               | await fileOperations.DeleteFileAsync("bucket-name", "object-key", cancellationToken);                                                              |   |
| DoseFileExistAsync    | Checks if a file exists in an S3 bucket.         | Provide the bucket name, object key, and a cancellation token. Returns a boolean indicating whether the file exists.                                         | var fileExists = await fileOperations.DoseFileExistAsync("bucket-name", "object-key", cancellationToken);                                          |   |
| GetFileAsync          | Gets a file from an S3 bucket as a stream.       | Provide the bucket name, object key, and a cancellation token. Returns a stream containing the file contents.                                                | var fileStream = await fileOperations.GetFileAsync("bucket-name", "object-key", cancellationToken);                                                |   |
| ListObjectsAsync      | Lists objects in an S3 bucket.                   | Provide the bucket name, prefix, delimiter, max keys, and a cancellation token. Returns an enumerable of object keys.                                        | var objectKeys = await fileOperations.ListObjectsAsync("bucket-name", "prefix", "delimiter", 1000, cancellationToken);                             |   |
| UploadFileStreamAsync | Uploads a file to an S3 bucket from a stream.    | Provide the bucket name, object key, stream containing the file contents, content type, and a cancellation token.                                            | await fileOperations.UploadFileStreamAsync("bucket-name", "object-key", fileStream, "text/plain", cancellationToken);                              |   |
| UploadSingleFileAsync | Uploads a file to an S3 bucket from a file path. | Provide the bucket name, source file path, destination object key, and a cancellation token. Returns a boolean indicating whether the upload was successful. | var uploadSuccessful = await fileOperations.UploadSingleFileAsync("bucket-name", "source-file-path", "destination-object-key", cancellationToken); |   |


### File Metadata Operations
| Method Name               | Description                                         | How to Use                                                                                                            | Example                                                                                                             |
|---------------------------|-----------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------|
| GetObjectMetadataAsync    | Gets the metadata for an object in an S3 bucket.    | Provide the bucket name, object key, and a cancellation token. Returns a dictionary containing the object's metadata. | var metadata = await fileMetadataOperations.GetObjectMetadataAsync("bucket-name", "object-key", cancellationToken); |
| UpdateObjectMetadataAsync | Updates the metadata for an object in an S3 bucket. | Provide the bucket name, object key, metadata dictionary, and a cancellation token.                                   | await fileMetadataOperations.UpdateObjectMetadataAsync("bucket-name", "object-key", metadata, cancellationToken);   |


### Large File Operations
| Method Name                                 | Description                                                  | How to Use                                                                                                                                           | Example                                                                                                                                   |
|---------------------------------------------|--------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| AbortMultipartUploadAsync                   | Aborts a multipart upload for a large file in an S3 bucket.  | Provide the bucket name, object key, upload ID, and a cancellation token.                                                                            | await largeFileOperations.AbortMultipartUploadAsync("bucket-name", "object-key", "upload-id", cancellationToken);                         |
| ~~UploadFullDirectoryAsync~~ (Pending)      | Uploads the contents of a local directory to an S3 bucket.   | Provide the bucket name, key prefix, local directory path, and a cancellation token. Returns a boolean indicating whether the upload was successful. | var success = await largeFileOperations.UploadFullDirectoryAsync("bucket-name", "key-prefix", "local-directory-path", cancellationToken); |
| ~~UploadLargeFileMultipartAsync~~ (Pending) | Uploads a large file to an S3 bucket using multipart upload. | Provide the bucket name, object key, input stream, part size, and a cancellation token.                                                              | await largeFileOperations.UploadLargeFileMultipartAsync("bucket-name", "object-key", inputStream, partSize, cancellationToken);           |.

### Presigned Url Operations

| Method Name                                   | Description                                                  | How to Use                                                                                                                           | Example                                                                                                                                                              |
|-----------------------------------------------|--------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ~~GeneratePresignedUrlForGetAsync~~ (Pending) | Generates a presigned URL for a GET request to an S3 object. | Provide the bucket name, object key, expiration time, and a cancellation token. Returns the presigned URL as a string.               | var url = await presignedUrlOperations.GeneratePresignedUrlForGetAsync("bucket-name", "object-key", DateTime.UtcNow.AddMinutes(5), cancellationToken);               |
| ~~GeneratePresignedUrlForPutAsync~~ (Pending) | Generates a presigned URL for a PUT request to an S3 object. | Provide the bucket name, object key, expiration time, content type, and a cancellation token. Returns the presigned URL as a string. | var url = await presignedUrlOperations.GeneratePresignedUrlForPutAsync("bucket-name", "object-key", DateTime.UtcNow.AddMinutes(5), "image/jpeg", cancellationToken); |
