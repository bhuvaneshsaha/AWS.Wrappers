
## List of Methods

### Operations Related to buckets
| Method Name                          | Description                                                                                |
|--------------------------------------|--------------------------------------------------------------------------------------------|
| CreateBucketAsync / CreateBucket     | It creates a bucket with the given name in the same region where the S3 object was created |
| DeleteBucketAsync / DeleteBucket     | Deletes the bucket if exists                                                               |
| IsBucketExistsAsync / IsBucketExists | Check if bucket exists in S3 or not.                                                       |
| ClearBucketAsync / ClearBucket	   | Clear object in bucket																		|

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
| UploadFileAsync / UploadFile	(Parameter: S3Upload)		   | Used to upload local file to S3																																				|
| UploadFileAsync / UploadFile (Parameter: S3UploadByBytes)	   | Used to upload file bytes to S3																																				|
| DownloadFileToLocalAsync / DownloadFileToLocal			   | Download S3 object(files) save it into the local path																															|
| GetFileBytesAsync / GetFileBytes							   | Get bytes of S3 object(files)																																					|
| IsFileExistsAsync / IsFileExists							   | Check file exists in S3 or not																																					|
| DeleteFileAsync / DeleteFile								   | Delete file if exists																																							|
| GetFilesAsync / GetFiles									   | Get list of files from S3 path																																					|


