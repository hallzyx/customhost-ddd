using customhost_backend.analytics.Domain.Models.Aggregates;
using customhost_backend.analytics.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.analytics.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// MetricData repository implementation using Entity Framework Core
/// </summary>
public class MetricDataRepository(AppDbContext context) : BaseRepository<MetricData>(context), IMetricDataRepository
{
    public async Task<IEnumerable<MetricData>> FindByMetricTypeAsync(string metricType)
    {
        return await Context.Set<MetricData>()
            .Where(m => m.MetricType == metricType)
            .OrderByDescending(m => m.PeriodStart)
            .ToListAsync();
    }

    public async Task<IEnumerable<MetricData>> FindByMetricTypeAndPeriodAsync(string metricType, DateTime fromDate, DateTime toDate)
    {
        return await Context.Set<MetricData>()
            .Where(m => m.MetricType == metricType 
                     && m.PeriodStart >= fromDate 
                     && m.PeriodEnd <= toDate)
            .OrderByDescending(m => m.PeriodStart)
            .ToListAsync();
    }

    public async Task<MetricData?> FindByMetricTypeAndExactPeriodAsync(string metricType, DateTime periodStart, DateTime periodEnd)
    {
        return await Context.Set<MetricData>()
            .FirstOrDefaultAsync(m => m.MetricType == metricType 
                                   && m.PeriodStart.Date == periodStart.Date 
                                   && m.PeriodEnd.Date == periodEnd.Date);
    }
}
