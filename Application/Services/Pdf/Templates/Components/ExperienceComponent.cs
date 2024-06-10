using Application.Services.Pdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class ExperienceComponent(IEnumerable<ExperienceDataItem> experiences) : IComponent
{
    private readonly IEnumerable<ExperienceDataItem> _experiences = experiences;

    public void Compose(IContainer container)
    {
        container.EnsureSpace().Column(column =>
        {
            column.Item().Component(new TitleComponent("Experience"));

            foreach (var experience in _experiences)
            {
                column.Item()
                    .BorderLeft(2).BorderColor(Colors.LightBlue.Darken2)
                    .PaddingLeft(10).PaddingVertical(10)
                    .Column(column => 
                    {
                        column.Item().Row(row =>
                        {
                            row.Spacing(10);

                            row.AutoItem().Text(experience.Company).FontSize(20);
                            if (!string.IsNullOrEmpty(experience.CompanyLink))
                            {
                                row.AutoItem()
                                    .AlignBottom()
                                    .Hyperlink(experience.CompanyLink)
                                    .Text(experience.CompanyLink)
                                    .FontColor(Colors.LightBlue.Darken2);
                            }
                        });

                        column.Item()
                            .PaddingBottom(5)
                            .Text(experience.Duration)
                            .FontColor(Colors.LightBlue.Darken2);

                        if (!string.IsNullOrEmpty(experience.Text))
                        {
                            column.Item().Text(experience.Text);
                        }
                    });
            }
        });
    }
}