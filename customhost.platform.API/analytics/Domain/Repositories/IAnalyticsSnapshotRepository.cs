using customhost_backend.analytics.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.analytics.Domain.Repositories;

/// <summary>
/// Repository interface for AnalyticsSnapshot aggregate
/// </summary>
public interface IAnalyticsSnapshotRepository : IBaseRepository<AnalyticsSnapshot>
{
    Task<AnalyticsSnapshot?> FindByTypeAsync(string snapshotType);
    Task<IEnumerable<AnalyticsSnapshot>> FindByTypeAndDateRangeAsync(string snapshotType, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<AnalyticsSnapshot>> FindExpiredAsync();
}
