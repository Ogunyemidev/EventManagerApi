using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyApi.Application;
using MyApi.Application.Storage;

namespace MyApi.Infrastructure.Storage
{
    public sealed class LocalFileStorage : IFileStorage
    {
        private readonly LocalFileStorageOptions _options;
        private readonly ILogger<LocalFileStorage> _logger;

        public LocalFileStorage(IOptions<FileStorageOptions> options, ILogger<LocalFileStorage> logger)
        {
            _options = options.Value.Local;
            _logger = logger;

            Directory.CreateDirectory(_options.RootPath);
        }

        public async Task<string> SaveAsync(FileUploadRequest request, CancellationToken cancellationToken = default)
        {
            var folder = string.IsNullOrWhiteSpace(request.Folder) ? "uploads" : request.Folder;
            var filePath = Path.GetFullPath(Path.Combine(_options.RootPath, folder, request.FileName));

            if (!_options.OverwriteExisting && File.Exists(filePath))
            {
                var uniqueName = $"{Path.GetFileNameWithoutExtension(request.FileName)}_{Guid.NewGuid():N}{Path.GetExtension(request.FileName)}";
                filePath = Path.GetFullPath(Path.Combine(_options.RootPath, folder, uniqueName));
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await request.Content.CopyToAsync(fileStream, cancellationToken);

            _logger.LogInformation("Saved file {FileName} to local storage", filePath);
            return filePath;
        }

        public Task<Stream?> GetAsync(string path, CancellationToken cancellationToken = default)
        {
            var exists = File.Exists(path);
            _logger.LogInformation("Fetching file {FileName} from local storage. Exists: {Exists}", path, exists);
            Stream? stream = exists ? File.OpenRead(path) : null;
            return Task.FromResult(stream);
        }

        public Task DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                _logger.LogInformation("Deleted file {FileName} from local storage", path);
            }

            return Task.CompletedTask;
        }
    }
}
