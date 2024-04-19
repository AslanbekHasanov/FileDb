//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

namespace FileDB.Services.Files
{
    internal interface IFileService
    {
        long GetFileSizeInProject(DirectoryInfo directory);
    }
}
