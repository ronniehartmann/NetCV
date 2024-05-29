using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Authentication.Stores;

public class ConfigurationStore(
    LockoutService lockoutService,
    IOptions<AdminUserConfig> options)
    : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserLockoutStore<IdentityUser>
{
    private readonly LockoutService _lockoutService = lockoutService;
    private readonly AdminUserConfig _adminUser = options.Value;

    public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        return;
    }

    public Task<IdentityUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var user = _adminUser.Username == userId ? new IdentityUser { Id = userId, UserName = _adminUser.Username } : null;
        return Task.FromResult(user);
    }

    public Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        var user = _adminUser.Username.ToUpper() == normalizedUserName ? new IdentityUser { UserName = _adminUser.Username } : null;
        return Task.FromResult(user);
    }

    public Task<string?> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName?.ToUpper());
    }

    public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName!);
    }

    public Task<string?> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetNormalizedUserNameAsync(IdentityUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(IdentityUser user, string? userName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public Task SetPasswordHashAsync(IdentityUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        var hash = _adminUser.Username == user.UserName ? _adminUser.PasswordHash : null;
        return Task.FromResult(hash);
    }

    public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    public Task<DateTimeOffset?> GetLockoutEndDateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(_lockoutService.GetLockoutEndDate());
    }

    public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
    {
        _lockoutService.SetLockoutEndDate(lockoutEnd);
        return Task.CompletedTask;
    }

    public Task<int> IncrementAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        _lockoutService.IncrementAccessFailedCount();
        return Task.FromResult(_lockoutService.GetAccessFailedCount());
    }

    public Task ResetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        _lockoutService.ResetAccessFailedCount();
        return Task.CompletedTask;
    }

    public Task<int> GetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(_lockoutService.GetAccessFailedCount());
    }

    public Task<bool> GetLockoutEnabledAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(_lockoutService.GetLockoutEndDate() > DateTimeOffset.Now);
    }

    public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled, CancellationToken cancellationToken)
    {
        _lockoutService.SetLockoutEnabled(enabled);
        return Task.CompletedTask;
    }
}