using customhost_backend.analytics.Domain.Models.Aggregates;
using customhost_backend.analytics.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.analytics.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// AnalyticsSnapshot repository implementation using Entity Framework Core
/// </summary>
public class AnalyticsSnapshotRepository(AppDbContext context) : BaseRepository<AnalyticsSnapshot>(context), IAnalyticsSnapshotRepository
{
    public async Task<AnalyticsSnapshot?> FindByTypeAsync(string snapshotType)
    {
        return await Context.Set<AnalyticsSnapshot>()
            .Where(s => s.SnapshotType == snapshotType)
            .OrderByDescending(s => s.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AnalyticsSnapshot>> FindByTypeAndDateRangeAsync(string snapshotType, DateTime fromDate, DateTime toDate)
    {
        return await Context.Set<AnalyticsSnapshot>()
            .Where(s => s.SnapshotType == snapshotType 
                     && s.Timestamp >= fromDate 
                     && s.Timestamp <= toDate)
            .OrderByDescending(s => s.Timestamp)
            .ToListAsync();
    }

    public async Task<IEnumerable<AnalyticsSnapshot>> FindExpiredAsync()
    {
        var now = DateTime.UtcNow;
        return await Context.Set<AnalyticsSnapshot>()
            .Where(s => s.ExpiresAt < now)
            .ToListAsync();
    }
}
