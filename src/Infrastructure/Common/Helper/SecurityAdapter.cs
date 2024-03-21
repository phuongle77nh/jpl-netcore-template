using JplSecurity;
using JplSecurity.Contract;
using System.Data;

namespace Jpl.MicroService.Infrastructure.Common.Helper
{
    public static class SecurityAdapter
    {
        public static async Task<GetPermissionByUserResponse> GetUsersSecured(IDbConnection connection, DefaultIdType userId)
        {
            return await JplSecurityHelper.GetPermissionByUserAsync(connection, new GetPermissionByUserRequest
            {
                ServiceName = "rs-api-user",
                ServiceEntityName = "User",
                ServiceEntitySchema = "User",
                UserId = userId,
            });
        }
    }
}