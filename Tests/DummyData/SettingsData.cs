using Domain.Models;

namespace Tests.DummyData;

public static class SettingsData
{
    public static readonly Settings DEFAULT_SETTINGS = new()
    {
        Id = 1,
        FavIconFileName = "favicon.ico",
        PortraitImageFileName = "portrait.png",
        BackgroundImageFileName = "background.png",
        OpenToWork = true,
        ShowFooter = true,
        ShowVersion = true,
        ShowPoweredByNetCv = true
    };

    public static readonly Settings EMPTY_SETTINGS = new()
    {
        Id = 1,
        FavIconFileName = null!,
        PortraitImageFileName = null!,
        BackgroundImageFileName = null!,
        OpenToWork = false,
        ShowFooter = false,
        ShowVersion = false,
        ShowPoweredByNetCv = false
    };
}
