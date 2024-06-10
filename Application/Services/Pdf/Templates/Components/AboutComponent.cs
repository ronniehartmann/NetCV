using Application.Services.Pdf.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class AboutComponent(string text) : IComponent
{
    private readonly string _text = text;

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Component(new TitleComponent("About Me"));

            column.Item()
                  .Text(_text);
        });
    }
}