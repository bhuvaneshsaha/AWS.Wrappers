
## Bucket Operations



## Directory Operations
In AWS S3, there is no concept of "directories" in the way that a file system on a computer would have directories. Instead, the illusion of directories or folders is created through the use of "/" in the key names of objects. For example, if you have an object with the key "mydirectory/myfile.txt", it appears as if the object "myfile.txt" is in the directory "mydirectory", but in reality, it's just a single object with a "/" in its key name.

Because of this, to "create a directory", you can simply create an object with a key that ends in a "/", which gives the illusion of an empty directory. Note that this isn't strictly necessary to create other objects within this "directory" -- you can create "mydirectory/myfile.txt" without first creating "mydirectory/" -- but it can be helpful for organization and for certain tools that interact with S3.