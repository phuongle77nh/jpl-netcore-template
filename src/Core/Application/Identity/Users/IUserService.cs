using Jpl.MicroService.Application.Common.Interfaces;
using System.Security.Claims;

namespace Jpl.MicroService.Application.Identity.Users;

public interface IUserService : ITransientService
{
    Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken);
    Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default);
    Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken);

}

public class UserService : IUserService
{
    public UserService()
    {
    }

    public Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        // TODO : call authentication api to get permission
        throw new NotImplementedException();
    }

    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default)
    {
        // TODO : call authentication api to get permission
        return true;
    }

    public Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}