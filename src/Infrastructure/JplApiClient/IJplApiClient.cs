using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Infrastructure.JplApiClient;

public interface IJplApiClient : ITransientService
{
    Task<bool> GetPermissionByUserAsync(PermissionRequest request);
}