### Presigned Url Operations

| Method Name                                   | Description                                                  | How to Use                                                                                                                           | Example                                                                                                                                                              |
|-----------------------------------------------|--------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| GeneratePresignedUrlForGetAsync (Pending) | Generates a presigned URL for a GET request to an S3 object. | Provide the bucket name, object key, expiration time, and a cancellation token. Returns the presigned URL as a string.               | var url = await presignedUrlOperations.GeneratePresignedUrlForGetAsync("bucket-name", "object-key", DateTime.UtcNow.AddMinutes(5), cancellationToken);               |
| GeneratePresignedUrlForPutAsync (Pending) | Generates a presigned URL for a PUT request to an S3 object. | Provide the bucket name, object key, expiration time, content type, and a cancellation token. Returns the presigned URL as a string. | var url = await presignedUrlOperations.GeneratePresignedUrlForPutAsync("bucket-name", "object-key", DateTime.UtcNow.AddMinutes(5), "image/jpeg", cancellationToken); |