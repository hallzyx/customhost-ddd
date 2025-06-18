using customhost_backend.analytics.Domain.Models.Queries;
using customhost_backend.analytics.Domain.Services;
using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.analytics.Interfaces.REST.Resources;

namespace customhost_backend.analytics.Application.Internal.QueryServices;

/// <summary>
/// Analytics metric query service implementation
/// </summary>
public class AnalyticsMetricQueryService(
    IBillingsContextFacade billingsContextFacade,
    ICrmContextFacade crmContextFacade
) : IAnalyticsMetricQueryService
{
    public async Task<MonthlyRevenueTrendResource> Handle(GetMonthlyRevenueTrendQuery query)
    {
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddMonths(-query.Months);
        
        var monthlyData = new List<MonthlyRevenueDataResource>();
        decimal totalRevenue = 0;

        for (int i = 0; i < query.Months; i++)
        {
            var currentMonth = startDate.AddMonths(i);
            var year = currentMonth.Year;
            var month = currentMonth.Month;
            
            var revenue = await billingsContextFacade.GetTotalRevenueForMonthAsync(year, month);
            var bookings = await billingsContextFacade.GetBookingsCountForMonthAsync(year, month);
            
            monthlyData.Add(new MonthlyRevenueDataResource(
                currentMonth.ToString("yyyy-MM"),
                revenue,
                bookings
            ));
            
            totalRevenue += revenue;
        }

        var averageRevenue = query.Months > 0 ? totalRevenue / query.Months : 0;
        var period = $"{startDate:yyyy-MM} to {endDate:yyyy-MM}";

        return new MonthlyRevenueTrendResource(
            period,
            monthlyData,
            totalRevenue,
            Math.Round(averageRevenue, 2)
        );
    }

    public async Task<MonthlyServiceRequestsBreakdownResource> Handle(GetMonthlyServiceRequestsBreakdownQuery query)
    {
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddMonths(-query.Months);
        
        var monthlyData = new List<MonthlyServiceRequestDataResource>();

        for (int i = 0; i < query.Months; i++)
        {
            var currentMonth = startDate.AddMonths(i);
            var year = currentMonth.Year;
            var month = currentMonth.Month;
            
            var breakdown = await crmContextFacade.GetServiceRequestsCountByTypeForMonthAsync(year, month);
            var total = breakdown.Values.Sum();
            
            monthlyData.Add(new MonthlyServiceRequestDataResource(
                currentMonth.ToString("yyyy-MM"),
                breakdown,
                total
            ));
        }

        var period = $"{startDate:yyyy-MM} to {endDate:yyyy-MM}";

        return new MonthlyServiceRequestsBreakdownResource(
            period,
            monthlyData
        );
    }
}
