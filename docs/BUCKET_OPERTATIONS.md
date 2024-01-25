
## Bucket Operations
| Method Name          | Description                                      | How to Use                                                                   | Example                                                                                              |   |
|----------------------|--------------------------------------------------|------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------|---|
| CreateBucketAsync    | Creates a new S3 bucket with the specified name. | CreateBucketAsync(string bucketName, CancellationToken cancellationToken)    | await bucketOperations.CreateBucketAsync("my-bucket-name", cancellationToken);                       |   |
| DeleteBucketAsync    | Deletes the specified S3 bucket.                 | DeleteBucketAsync(string bucketName, CancellationToken cancellationToken)    | await bucketOperations.DeleteBucketAsync("my-bucket-name", cancellationToken);                       |   |
| DoesBucketExistAsync | Checks if the specified S3 bucket exists.        | DoesBucketExistAsync(string bucketName, CancellationToken cancellationToken) | var bucketExists = await bucketOperations.DoesBucketExistAsync("my-bucket-name", cancellationToken); |   |
| ListBucketsAsync     | Lists all S3 buckets in the account.             | ListBucketsAsync(CancellationToken cancellationToken)                        | var buckets = await bucketOperations.ListBucketsAsync(cancellationToken);                            |   |
| EmptyBucketAsync     | Deletes all objects in the specified S3 bucket.  | EmptyBucketAsync(string bucketName, CancellationToken cancellationToken)     | await bucketOperations.EmptyBucketAsync("my-bucket-name", cancellationToken);                        |   |