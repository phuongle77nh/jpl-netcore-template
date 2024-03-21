namespace JplSecurity.Contract;

public class JplSecurityItem
{
    public JplSecurityItem()
    {
        Permission = 0;
    }

    public Guid EntityId { get; set; }
    public int Permission { get; set; }
}
