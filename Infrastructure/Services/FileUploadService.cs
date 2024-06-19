using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class FileUploadService(ILogger<FileUploadService> logger)
{
    private readonly ILogger<FileUploadService> _logger = logger;

    public async Task<bool> UploadFileAsync(Stream stream, string fullPath, int maxFileSize)
    {
        if (stream.Length > maxFileSize)
        {
            _logger.LogError(
                "Image could not be uploaded. File size ({} bytes) exceeds maximum size of {} bytes.", 
                stream.Length, maxFileSize);

            return false;
        }

        using var destinationStream = new FileStream(fullPath, FileMode.Create);
        await stream.CopyToAsync(destinationStream);
        return true;
    }

    public void RemoveFile(string fullPath)
    {
        if (!File.Exists(fullPath))
        {
            return;
        }

        File.Delete(fullPath);
    }
}
