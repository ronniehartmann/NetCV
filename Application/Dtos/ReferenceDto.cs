using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class ReferenceDto : Resource<Reference>
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Employment { get; set; }

    public string Text { get; set; } = string.Empty;

    public override bool IsEqualToModel(Reference model)
    {
        return string.Equals(Title, model.Title)
            && string.Equals(Employment, model.Employment)
            && string.Equals(Text, model.Text);
    }
}