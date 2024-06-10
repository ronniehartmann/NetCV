using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class ReferenceDto : Resource<Reference>
{
    public long Id { get; set; }

    public required string Title { get; set; }

    public string? Employment { get; set; }

    public required string Text { get; set; }

    public override bool IsEqualToModel(Reference model)
    {
        return string.Equals(Title, model.Title)
            && string.Equals(Employment, model.Employment)
            && string.Equals(Text, model.Text);
    }
}