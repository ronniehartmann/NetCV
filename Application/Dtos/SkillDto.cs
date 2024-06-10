using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class SkillDto : Resource<Skill>
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Level { get; set; }

    public override bool IsEqualToModel(Skill model)
    {
        return Level == model.Level
            && string.Equals(Name, model.Name);
    }
}
