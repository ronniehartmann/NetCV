using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Stores;

public class DisabledRoleStore : IRoleStore<IdentityRole>
{
    private const string ROLES_NOT_SUPPORTED = "Roles are not supported.";

    public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<string?> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task SetRoleNameAsync(IdentityRole role, string? roleName, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<string?> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<IdentityRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public Task<IdentityRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new NotSupportedException(ROLES_NOT_SUPPORTED);
    }

    public void Dispose()
    {
        return;
    }
}
