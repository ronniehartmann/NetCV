using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class TitleComponent(string title) : IComponent
{
    private string _title = title;

    public void Compose(IContainer container)
    {
        container.PaddingVertical(20)
                 .Text(_title)
                 .FontSize(26);
    }
}