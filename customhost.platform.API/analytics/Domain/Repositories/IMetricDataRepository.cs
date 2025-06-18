using customhost_backend.analytics.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.analytics.Domain.Repositories;

/// <summary>
/// Repository interface for MetricData aggregate
/// </summary>
public interface IMetricDataRepository : IBaseRepository<MetricData>
{
    Task<IEnumerable<MetricData>> FindByMetricTypeAsync(string metricType);
    Task<IEnumerable<MetricData>> FindByMetricTypeAndPeriodAsync(string metricType, DateTime fromDate, DateTime toDate);
    Task<MetricData?> FindByMetricTypeAndExactPeriodAsync(string metricType, DateTime periodStart, DateTime periodEnd);
}
