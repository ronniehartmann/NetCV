using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Authentication.Stores;

public class MonoConfigurationUserStore(
    LockoutService lockoutService,
    IOptions<AdminUserConfig> options)
    : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserLockoutStore<IdentityUser>
{
    private readonly LockoutService _lockoutService = lockoutService;
    private readonly AdminUserConfig _adminUser = options.Value;

    private const string USER_EDIT_NOT_SUPPORTED = "Editing users is not supported.";

    #region Supported Methods
    public Task<IdentityUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (string.Equals(_adminUser.Username, userId))
        {
            var user = new IdentityUser
            {
                Id = userId,
                UserName = userId
            } ?? null;
            return Task.FromResult(user);
        }

        return Task.FromResult<IdentityUser?>(null);
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

    public Task SetNormalizedUserNameAsync(IdentityUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName!);
    }

    public Task<string?> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
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

    public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityResult.Success);
    }

    public void Dispose()
    {
        return;
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
        return Task.CompletedTask;
    }

    public Task<DateTimeOffset?> GetLockoutEndDateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(_lockoutService.GetLockoutEndDate());
    }
    #endregion

    #region Unsupported Methods
    public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(USER_EDIT_NOT_SUPPORTED);
    }

    public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(USER_EDIT_NOT_SUPPORTED);
    }

    public Task SetUserNameAsync(IdentityUser user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(USER_EDIT_NOT_SUPPORTED);
    }

    public Task SetPasswordHashAsync(IdentityUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(USER_EDIT_NOT_SUPPORTED);
    }
    #endregion
}