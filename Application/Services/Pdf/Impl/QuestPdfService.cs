using Application.Services.Contents;
using QuestPDF.Fluent;

namespace Application.Services.Pdf.Impl;

public class QuestPdfService(IContentService contentService) : IPdfService
{
    private readonly IContentService _contentService = contentService;

    public byte[] GeneratePdf()
    {
        var pdfBytes = Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Content()
                    .Element(e =>
                    {
                        e.Image("/images/portrait.jpg");
                        e.Border(2);
                    });

                page.Content()
                    .Element(async e =>
                    {
                        var fullName = await _contentService.GetValueAsync("PROFILE_FULLNAME");
                        e.Text(fullName);
                    });
            });
        }).GeneratePdf();

        return pdfBytes;
    }
}
