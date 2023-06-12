

# **BucketOperations**


| Method | Description |
| --- | --- |
| BucketOperations(IAmazonS3 s3Client) | Creates a new instance of the BucketOperations class with the specified Amazon S3 client. |
| CreateBucket(string bucketName) | Creates a new Amazon S3 bucket with the specified name. |
| CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default) | Creates a new Amazon S3 bucket with the specified name asynchronously. |
| DeleteBucket(string bucketName) | Deletes an Amazon S3 bucket with the specified name. |
| DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default) | Deletes an Amazon S3 bucket with the specified name asynchronously. |
| ListBuckets() | Returns a list of the names of all Amazon S3 buckets in the current account. |
| ListBucketsAsync(CancellationToken cancellationToken = default) | Returns a list of the names of all Amazon S3 buckets in the current account asynchronously. |

The `BucketOperations` class provides methods for creating, deleting, and listing Amazon S3 buckets.

## **Constructor**

```C#
public BucketOperations(IAmazonS3 s3Client)
```

Creates a new instance of the `BucketOperations` class with the specified Amazon S3 client.

### **Parameters**

- `s3Client` - An instance of the `IAmazonS3` interface that represents the Amazon S3 client to use for operations.

## **Methods**

### **CreateBucket**

```C#
public void CreateBucket(string bucketName)
```

Creates a new Amazon S3 bucket with the specified name.

### **Parameters**

- `bucketName` - A string that represents the name of the bucket to create.

### **Exceptions**

- `AmazonS3Exception` - If the bucket creation fails.

### **CreateBucketAsync**

```C#
public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
```

Creates a new Amazon S3 bucket with the specified name asynchronously.

### **Parameters**

- `bucketName` - A string that represents the name of the bucket to create.
- `cancellationToken` - A `CancellationToken` that can be used to cancel the operation.

### **Exceptions**

- `AmazonS3Exception` - If the bucket creation fails.

### **DeleteBucket**
```C#
public void DeleteBucket(string bucketName)
```

Deletes an Amazon S3 bucket with the specified name.

### **Parameters**

- `bucketName` - A string that represents the name of the bucket to delete.

### **Exceptions**

- `AmazonS3Exception` - If the bucket deletion fails.

### **DeleteBucketAsync**
```C#
public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
```

Deletes an Amazon S3 bucket with the specified name asynchronously.

### **Parameters**

- `bucketName` - A string that represents the name of the bucket to delete.
- `cancellationToken` - A `CancellationToken` that can be used to cancel the operation.

### **Exceptions**

- `AmazonS3Exception` - If the bucket deletion fails.

### **ListBuckets**
```C#
public IEnumerable<string> ListBuckets()
```

Returns a list of the names of all Amazon S3 buckets in the current account.

### **Returns**

- An `IEnumerable<string>` that represents the names of all Amazon S3 buckets in the current account.

### **ListBucketsAsync**
```C#
public async Task<IEnumerable<string>> ListBucketsAsync(CancellationToken cancellationToken = default)
```

Returns a list of the names of all Amazon S3 buckets in the current account asynchronously.

### **Parameters**

- `cancellationToken` - A `CancellationToken` that can be used to cancel the operation.

### **Returns**

- An `IEnumerable<string>` that represents the names of all Amazon S3 buckets in the current account.


