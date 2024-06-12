using Application.Services.Pdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class ReferenceComponent(IEnumerable<ReferenceDataItem> references) : IComponent
{
    private readonly IEnumerable<ReferenceDataItem> _references = references;

    public void Compose(IContainer container)
    {
        container.EnsureSpace().Column(column =>
        {
            column.Item().Component(new TitleComponent("References"));
            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                foreach (var reference in _references)
                {
                    table.Cell().Column(referenceColumn =>
                    {
                        referenceColumn.Item().Text(reference.Title).Bold();
                        if (!string.IsNullOrEmpty(reference.Employment))
                        {
                            referenceColumn.Item()
                                .Text(reference.Employment)
                                .FontColor(Colors.LightBlue.Darken2);
                        }
                        referenceColumn.Item()
                            .PaddingTop(5).PaddingBottom(10)
                            .BorderLeft(2).BorderColor(Colors.LightBlue.Darken2)
                            .PaddingLeft(10)
                            .Text($"\"{reference.Text}\"");
                    });
                }
            });
        });
    }
}