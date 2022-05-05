using AWS.Wrappers.S3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Wrappers.S3.Interfaces;

public interface IS3Service : IS3BucketService, IS3BucketServiceAsync,
                              IS3DirectoryService, IS3DirectoryServiceAsync,
                              IS3FileService, IS3FileServiceAsync
{
}
