namespace Domain.Models;

public class Settings
{
    public long Id { get; set; }

    public required string PortraitImageFileName { get; set; }

    public required string BackgroundImageFileName { get; set; }

    public required bool ShowFooter { get; set; }

    public required bool ShowVersion { get; set; }

    public required bool ShowPoweredByNetCv { get; set; }
}
