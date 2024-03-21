namespace JplSecurity.Contract;

public class GetPermissionByUserResponse
{
    public GetPermissionByUserResponse()
    {
        securedItems = new List<JplSecurityItem>();
    }

    public List<JplSecurityItem> securedItems { get; set; }
}
