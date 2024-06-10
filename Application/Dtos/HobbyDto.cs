using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class HobbyDto : Resource<Hobby>
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Icon { get; set; }

    public override bool IsEqualToModel(Hobby model)
    {
        return string.Equals(Name, model.Text)
            && string.Equals(Icon, model.Icon);
    }
}
