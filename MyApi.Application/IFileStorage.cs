using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MyApi.Application
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(FileUploadRequest request, CancellationToken cancellationToken = default);
        Task<Stream?> GetAsync(string path, CancellationToken cancellationToken = default);
        Task DeleteAsync(string path, CancellationToken cancellationToken = default);
    }

    public class FileUploadRequest
    {
        public Stream Content { get; set; } = Stream.Null;
        public string FileName { get; set; } = string.Empty;
        public string Folder { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/octet-stream";
    }

    public enum FileStorageProvider
    {
        Local,
        AwsS3,
        AzureBlob
    }

    public class FileStorageOptions
    {
        public FileStorageProvider Provider { get; set; } = FileStorageProvider.Local;
        public LocalFileStorageOptions Local { get; set; } = new();
        public AwsS3FileStorageOptions Aws { get; set; } = new();
        public AzureBlobStorageOptions Azure { get; set; } = new();
    }

    public class LocalFileStorageOptions
    {
        public string RootPath { get; set; } = Path.Combine(AppContext.BaseDirectory, "uploads");
        public bool OverwriteExisting { get; set; } = false;
    }

    public class AwsS3FileStorageOptions
    {
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Region { get; set; } = "us-east-1";
        public string BucketName { get; set; } = string.Empty;
    }

    public class AzureBlobStorageOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string ContainerName { get; set; } = string.Empty;
    }
}
