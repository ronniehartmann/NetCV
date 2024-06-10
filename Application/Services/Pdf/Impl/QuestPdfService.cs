using Application.Services.Contents;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace Application.Services.Pdf.Impl;

public class QuestPdfService(IContentService contentService) : IPdfService
{
    private readonly IContentService _contentService = contentService;

    public async Task<byte[]> GeneratePdfAsync()
    {
        var portraitBytes = File.ReadAllBytes("wwwroot/images/portrait.jpg");
        var fullName = await _contentService.GetValueAsync("PROFILE_FULLNAME");
        var employment = await _contentService.GetValueAsync("PROFILE_EMPLOYMENT");
        var aboutText = await _contentService.GetValueAsync("ABOUT_TEXT");

        QuestPDF.Settings.License = LicenseType.Community;

        var bytes = Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Margin(32);
                page.Content()
                    .Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .AlignCenter()
                                .Width(300)
                                .Padding(30)
                                .Image(portraitBytes);
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Text(fullName)
                                .FontSize(36)
                                .AlignCenter();
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .PaddingBottom(30)
                                .Text(employment)
                                .FontSize(22)
                                .AlignCenter();
                        });

                        column.Item().Row(row =>
                        {
                            row.Spacing(10);

                            row.RelativeItem()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("About Me")
                                        .FontSize(22);

                                    column.Item()
                                        .Text(aboutText);
                                });

                            row.RelativeItem()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("Bio")
                                        .FontSize(22);

                                    column.Item()
                                        .Text("-");
                                });
                        });                        
                    });
            });
        }).GeneratePdf();

        return bytes;
    }
}
