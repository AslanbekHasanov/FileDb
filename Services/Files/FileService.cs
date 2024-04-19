//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------

using FileDB.Brokers.Loggings;

namespace FileDB.Services.Files
{
    internal class FileService : IFileService
    {
        private readonly ILoggingBroker broker;
        private long size;

        public FileService()
        {
            this.broker = new LoggingBroker();
            this.size = 0;
        }
        public long GetFileSizeInProject(DirectoryInfo directory)
        {
            try
            {
                var fileInformation = directory.GetFiles();
                var folders = directory.GetDirectories();

                foreach (var file in fileInformation)
                {
                    size += file.Length;
                }

                foreach (var folder in folders)
                {
                    size += GetFileSizeInProject(folder);
                }

                this.broker.LogInformation($"File size: {size} bytes");
            }
            catch (Exception ex)
            {
                this.broker.LogError(ex.Message);
            }

            return size;
        }
    }
}
