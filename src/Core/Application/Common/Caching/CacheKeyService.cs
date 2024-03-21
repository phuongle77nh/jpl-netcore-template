using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Application.Common.Caching;

public interface ICacheKeyService : IScopedService
{
    public string GetCacheKey(string name, object id, bool includeTenantId = true);
}