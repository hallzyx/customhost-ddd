namespace customhost_backend.analytics.Interfaces.REST.Resources;

/// <summary>
/// Resource for monthly revenue trend response
/// </summary>
public record MonthlyRevenueTrendResource(
    string Period,
    IEnumerable<MonthlyRevenueDataResource> Data,
    decimal TotalRevenue,
    decimal AverageMonthlyRevenue
);

/// <summary>
/// Resource for individual month revenue data
/// </summary>
public record MonthlyRevenueDataResource(
    string Month,
    decimal Revenue,
    int Bookings
);
