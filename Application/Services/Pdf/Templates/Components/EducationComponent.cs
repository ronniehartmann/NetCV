using Application.Services.Pdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class EducationComponent(IEnumerable<EducationDataItem> educations) : IComponent
{
    private readonly IEnumerable<EducationDataItem> _educations = educations;

    public void Compose(IContainer container)
    {
        container.EnsureSpace(200).Column(column =>
        {
            column.Item().Component(new TitleComponent("Education"));

            foreach (var education in _educations)
            {
                column.Item()
                    .BorderLeft(2).BorderColor(Colors.LightGreen.Darken2)
                    .PaddingLeft(10).PaddingVertical(10)
                    .Column(column =>
                    {
                        column.Item().Text(education.School).FontSize(20);

                        column.Item()
                            .PaddingBottom(5)
                            .Text(education.Duration)
                            .FontColor(Colors.LightGreen.Darken2);

                        column.Item().Text(education.Title);
                    });
            }
        });
    }
}