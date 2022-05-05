
## List of Methods

### Operations Related to buckets
| Method Name                          | Description                                                                                |
|--------------------------------------|--------------------------------------------------------------------------------------------|
| CreateBucketAsync / CreateBucket     | It creates a bucket with the given name in the same region where the S3 object was created |
| DeleteBucketAsync / DeleteBucket     | Deletes the bucket if exists                                                               |
| IsBucketExistsAsync / IsBucketExists | Check if bucket exists in S3 or not.                                                       |

### Operations Related to directory/folder _(S3 Objects)_

| Method Name                                                  | Description                                                                                                                                                                    |
|--------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| IsDirectoryExistsAsync / IsDirectoryExists                   | Checks Folder(Key) exists in S3 Bucket                                                                                                                                         |
| CreateDirectoryAsync / CreateDirectory                       | Create directory inside give bucket if not exits.                                                                                                                              |
| DeleteDirectoryAsync / DeleteDirectory                       | If a folder has files or sub-folders in it, then it won't be deleted​. We need to pass recursive = true in the argument to delete all files and folders inside the given path. |
| GetSubDirectoriesAsync / GetSubDirectories                   | It gets the list of subdirectories from the given path.                                                                                                                        |
| GetAllDirectoriesRecursiveAsync / GetAllDirectoriesRecursive | It gets the list of directories and sub-directories under the given path.                                                                                                      |


### Operations Related to files _(S3 Objects)_

| Method Name                                                  | Description                                                                                                                                                                    |
|--------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| UploadFileAsync / UploadFile				                   | Used to upload local file to S3																																				|


