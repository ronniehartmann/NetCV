using Microsoft.AspNetCore.Components.Forms;

namespace NetCV.Extensions;

public static class BrowserFileExtensions
{
    public static async Task<bool> IsImageAsync(this IBrowserFile browserFile)
    {
        if (!browserFile.ContentType.StartsWith("image/"))
        {
            return false;
        }

        if (!await browserFile.HasImageContentAsync())
        {
            return false;
        }

        return true;
    }

    private static async Task<bool> HasImageContentAsync(this IBrowserFile browserFile)
    {
        try
        {
            var signatures = new List<byte[]>
            {
                // JPG
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                // PNG
                new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                // ICO
                new byte[] { 0x00, 0x00, 0x01, 0x00 }
            };

            using var memoryStream = new MemoryStream();
            using var fileStream = browserFile.OpenReadStream(2097152);
            await fileStream.CopyToAsync(memoryStream);

            var headerBytes = new byte[signatures.Max(s => s.Length)];
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.ReadAsync(headerBytes, 0, signatures.Max(s => s.Length));

            return signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in HasImageContent: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            return false;
        }
    }
}
