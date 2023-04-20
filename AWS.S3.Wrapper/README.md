

## Available Methods
| Method Name                     | Description                                                 | Usage                                                                                                            |
| ------------------------------- | ----------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| `CreateBucket`                  | Creates a new bucket.                                       | `CreateBucket("my-bucket");`                                                                                     |
| `ListBuckets`                   | Lists all buckets.                                          | `IEnumerable<string> buckets = ListBuckets();`                                                                   |
| `DeleteBucket`                  | Deletes the specified bucket.                               | `DeleteBucket("my-bucket");`                                                                                     |
| `PutFile`                       | Puts a file into a bucket.                                  | `PutFile("my-bucket", "object-key", stream, "image/jpeg");`                                                      |
| `GetFile`                       | Gets a file from a bucket.                                  | `Stream fileStream = GetFile("my-bucket", "object-key");`                                                        |
| `DeleteFile`                    | Deletes a file from a bucket.                               | `DeleteFile("my-bucket", "object-key");`                                                                         |
| `CopyFile`                      | Copies a file from one location to another within a bucket. | `CopyFile("source-bucket", "source-key", "destination-bucket", "destination-key");`                              |
| `ListObjects`                   | Lists objects in a bucket.                                  | `IEnumerable<string> objects = ListObjects("my-bucket", "prefix/", "/", 100);`                                   |
| `GetObjectMetadata`             | Gets the metadata of an object.                             | `IDictionary<string, string> metadata = GetObjectMetadata("my-bucket", "object-key");`                           |
| `UpdateObjectMetadata`          | Updates the metadata of an object.                          | `UpdateObjectMetadata("my-bucket", "object-key", metadata);`                                                     |
| `GeneratePresignedUrlForGet`    | Generates a pre-signed URL for getting an object.           | `string url = GeneratePresignedUrlForGet("my-bucket", "object-key", DateTime.UtcNow.AddHours(1));`               |
| `GeneratePresignedUrlForPut`    | Generates a pre-signed URL for putting an object.           | `string url = GeneratePresignedUrlForPut("my-bucket", "object-key", DateTime.UtcNow.AddHours(1), "image/jpeg");` |
| `UploadLargeFileMultipartAsync` | Uploads a large file to a bucket using multipart upload.    | `await UploadLargeFileMultipartAsync("my-bucket", "object-key", stream);`                                        |
| `AbortMultipartUploadAsync`     | Aborts an ongoing multipart upload.                         | `await AbortMultipartUploadAsync("my-bucket", "object-key", "upload-id");`                                       |
| `CreateDirectory`               | Creates a directory in a bucket.                            | `CreateDirectory("my-bucket", "directory-path");`                                                                |
| `ListDirectories`               | Lists directories within a bucket.                          | `IEnumerable<string> directories = ListDirectories("my-bucket", "parent-directory-path");`                       |
| `DeleteDirectory`               | Deletes a directory from a bucket.                          | `DeleteDirectory("my-bucket", "directory-path");`                                                                |


> The library contains both synchronous and asynchronous methods.
>> For the asynchronous methods, you can replace the method names with the Async version and use await before calling them.

<p>Amazon S3 does not have a native concept of folders or directories like a traditional file system. However, we create a pseudo-directory structure using prefixes and delimiters in object keys. This approach is commonly used to organize objects within a bucket.

Here are some methods you can add to your S3Wrapper to handle path/directory related operations:

**CreateDirectory**: Create an empty "directory" by uploading an object with a zero-byte size and a key that ends with a delimiter (usually /).

**ListDirectories**: List "directories" within a bucket or a "parent" directory by using the prefix and delimiter options while listing objects.

**DeleteDirectory**: Delete a "directory" by deleting all objects with the specified prefix.
</p>