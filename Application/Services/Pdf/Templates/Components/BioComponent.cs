using Application.Services.Pdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class BioComponent(IEnumerable<BioDataItem> data) : IComponent
{
    private readonly IEnumerable<BioDataItem> _data = data;

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Component(new TitleComponent("Bio"));

            foreach (var item in _data)
            {
                var icon = SvgImage.FromFile(item.IconPath);

                column.Item().PaddingBottom(10).Row(bioRow =>
                {
                    bioRow.RelativeItem(1).PaddingRight(12).PaddingBottom(2).Svg(icon).FitWidth();
                    bioRow.RelativeItem(2).Text(item.Title).Bold();
                    bioRow.RelativeItem(6).Text(item.Text);
                });
            }
        });
    }
}