namespace JplSecurity.Api.Client;

public interface IJplSecurityApiClient
{
    Task<GetPermissionByUserResponse> GetPermissionByUserAsync(GetPermissionByUserRequest request);
}