
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