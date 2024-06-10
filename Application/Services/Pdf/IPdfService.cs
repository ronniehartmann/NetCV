namespace Application.Services.Pdf;

public interface IPdfService
{
    Task<byte[]> GeneratePdfAsync();
}
