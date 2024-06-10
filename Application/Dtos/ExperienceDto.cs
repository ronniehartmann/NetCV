using Application.Services.Resources.Models;
using Domain.Models;

namespace Application.Dtos;

public class ExperienceDto : Resource<Experience>
{
    public long Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Company { get; set; } = string.Empty;

    public string? CompanyLink { get; set; }

    public string? Text { get; set; }

    public override bool IsEqualToModel(Experience model)
    {
        return StartDate == model.StartDate
            && EndDate == model.EndDate
            && string.Equals(Company, model.Company)
            && string.Equals(CompanyLink, model.CompanyLink);
    }
}
