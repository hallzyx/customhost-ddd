using customhost_backend.analytics.Domain.Models.Queries;
using customhost_backend.analytics.Interfaces.REST.Resources;

namespace customhost_backend.analytics.Domain.Services;

/// <summary>
/// Analytics query service interface for historical metric data
/// </summary>
public interface IAnalyticsMetricQueryService
{
    Task<MonthlyRevenueTrendResource> Handle(GetMonthlyRevenueTrendQuery query);
    Task<MonthlyServiceRequestsBreakdownResource> Handle(GetMonthlyServiceRequestsBreakdownQuery query);
}
