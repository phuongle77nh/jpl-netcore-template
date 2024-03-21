namespace Jpl.MicroService.Host.Controllers;

public class VersionController : VersionNeutralApiController
{
    public VersionController()
    {

    }

    [HttpGet]
    [Authorize]
    [MustHavePermission(FSHAction.View, FSHResource.Tenants)]
    [OpenApiOperation("Get version of api.", "")]
    public ActionResult<string> GetVersion()
    {
        return "0.1";
    }
}