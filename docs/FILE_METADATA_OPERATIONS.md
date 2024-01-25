
### File Metadata Operations
| Method Name               | Description                                         | How to Use                                                                                                            | Example                                                                                                             |
|---------------------------|-----------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------|
| GetObjectMetadataAsync    | Gets the metadata for an object in an S3 bucket.    | Provide the bucket name, object key, and a cancellation token. Returns a dictionary containing the object's metadata. | var metadata = await fileMetadataOperations.GetObjectMetadataAsync("bucket-name", "object-key", cancellationToken); |
| UpdateObjectMetadataAsync | Updates the metadata for an object in an S3 bucket. | Provide the bucket name, object key, metadata dictionary, and a cancellation token.                                   | await fileMetadataOperations.UpdateObjectMetadataAsync("bucket-name", "object-key", metadata, cancellationToken);   |

