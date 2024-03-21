namespace JplSecurity.Contract;

public class SecuredItem
{
    public Guid EntityId { get; set; }
    public int Permission { get; set; }
}
