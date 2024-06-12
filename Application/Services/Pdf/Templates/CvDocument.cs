using Application.Services.Pdf.Models;
using Application.Services.Pdf.Templates.Components;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates;

public class CvDocument(PdfData data) : IDocument
{
    private readonly PdfData _data = data;
    private readonly TextStyle _defaultTextStyle = new TextStyle().FontSize(13).FontFamily("arial");

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        var portraitBytes = File.ReadAllBytes("wwwroot/images/portrait.jpg");

        container.Page(page =>
        {
            page.DefaultTextStyle(_defaultTextStyle);
            page.Margin(32);
            page.Content()
                .Column(column =>
                {
                    column.Item().Component(new ProfileComponent(portraitBytes, _data.FullName, _data.Employment));

                    column.Item().Row(row =>
                    {
                        row.Spacing(32);

                        row.RelativeItem(4).Component(new AboutComponent(_data.AboutText));
                        row.RelativeItem(5).Component(new BioComponent(_data.Bio));
                    });

                    column.Item().Component(new ListComponent("Hobbies", _data.Hobbies));
                    column.Item().Component(new ListComponent("Professional Skills", _data.Skills));
                    column.Item().Component(new ExperienceComponent(_data.Experiences));
                    column.Item().Component(new EducationComponent(_data.Educations));
                    column.Item().Component(new ReferenceComponent(_data.References));
                });
        });
    }
}