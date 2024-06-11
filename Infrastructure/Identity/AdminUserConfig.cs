namespace Infrastructure.Identity;

public class AdminUserConfig
{
    public required string Username { get; set; }

    public required string PasswordHash { get; set; }
}