using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class ProfileComponent(byte[] portrait, string fullName, string employment) : IComponent
{
    private readonly byte[] _portrait = portrait;
    private readonly string _fullName = fullName;
    private readonly string _employment = employment;

    public void Compose(IContainer container)
    {
        container.PaddingBottom(40).Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .AlignCenter()
                    .Width(300)
                    .Padding(30)
                    .Image(_portrait);
            });

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .Text(_fullName)
                    .FontSize(36)
                    .AlignCenter();
            });

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .PaddingBottom(30)
                    .Text(_employment)
                    .FontSize(22)
                    .FontColor(Colors.LightBlue.Darken2)
                    .AlignCenter();
            });
        });
    }
}