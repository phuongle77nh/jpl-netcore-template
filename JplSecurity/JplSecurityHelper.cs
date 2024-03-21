using Dapper;
using JplSecurity.Contract;
using System.Data;

namespace JplSecurity;

public static class JplSecurityHelper
{
    public static async Task<GetPermissionByUserResponse> GetPermissionByUserAsync(IDbConnection connection, GetPermissionByUserRequest request)
    {
        var result = await connection.QueryAsync<SecuredItem>("rs_security.[Grant].[GetPermissionByUserId]", new
        {
            ServiceName = request.ServiceName,
            InputTableName = request.ServiceEntityName,
            InputTableSchema = request.ServiceEntitySchema,
            InputUserId = request.UserId
        }, commandType: CommandType.StoredProcedure);

        var response = new GetPermissionByUserResponse
        {
            securedItems = result == null 
            ? new List<JplSecurityItem>() 
            : result.Select(x => new JplSecurityItem { 
                EntityId = x.EntityId, 
                Permission = x.Permission 
            }).ToList()
        };

        return response;
    }

    public static async Task<List<Guid>> GetMemberByManagerId(IDbConnection connection, Guid managerId)
    {
        var result = await connection.QueryAsync<Guid>("rs_security.[Grant].[GetMemberByManagerId]", new
        {
            ManagerId = managerId
        }, commandType: CommandType.StoredProcedure);

        return result.ToList();
    }
}