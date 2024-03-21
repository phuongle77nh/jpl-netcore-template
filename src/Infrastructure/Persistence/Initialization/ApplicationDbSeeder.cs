using Jpl.MicroService.Infrastructure.Multitenancy;
using Jpl.MicroService.Infrastructure.Persistence.Context;
using Jpl.MicroService.Shared.Authorization;
using Jpl.MicroService.Shared.Multitenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jpl.MicroService.Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }
}