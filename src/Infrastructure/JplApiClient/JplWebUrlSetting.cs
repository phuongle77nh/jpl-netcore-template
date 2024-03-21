using System.Net;

namespace Jpl.MicroService.Infrastructure.JplApiUrl;

public class JplWebUrlSetting
{
    public class ResetPasswordItem
    {
        public string Action { get; set; }
        public List<string> Params { get; set; }
    }

    public string BaseUrl { get; set; } = string.Empty;
    public ResetPasswordItem ResetPassword { get; set; }
}