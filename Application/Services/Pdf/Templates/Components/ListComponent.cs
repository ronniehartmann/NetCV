using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Application.Services.Pdf.Templates.Components;

public class ListComponent(string title, IEnumerable<string> hobbies) : IComponent
{
    private readonly string _title = title;
    private readonly IEnumerable<string> _hobbies = hobbies;

    public void Compose(IContainer container)
    {
        container.EnsureSpace().Column(column =>
        {
            column.Item().Component(new TitleComponent(_title));

            foreach (var hobby in _hobbies)
            {
                column.Item().PaddingBottom(10).Text(" •  " + hobby).FontSize(15);
            }
        });
    }
}