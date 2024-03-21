using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Application.Auditing;

public interface IAuditService : ITransientService
{
    Task<List<AuditDto>> GetUserTrailsAsync(DefaultIdType userId);
}