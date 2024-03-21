namespace JplSecurity.Contract;

public class GetPermissionByUserRequest
{
    public Guid UserId { get; set; }
    public string ServiceName { get; set; }
    public string ServiceEntityName { get; set; }
    public string ServiceEntitySchema { get; set; }
    public GetPermissionByUserRequest()
    {
        ServiceName = string.Empty;
        ServiceEntityName = string.Empty;
        ServiceEntitySchema = string.Empty;
    }
}
