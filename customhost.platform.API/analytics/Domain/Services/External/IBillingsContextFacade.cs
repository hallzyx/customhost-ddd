namespace customhost_backend.analytics.Domain.Services.External;

/// <summary>
/// Anti-Corruption Layer facade for accessing Billings context data
/// </summary>
public interface IBillingsContextFacade
{
    Task<decimal> GetTotalRevenueForMonthAsync(int year, int month);
    Task<int> GetBookingsCountForMonthAsync(int year, int month);
    Task<Dictionary<string, decimal>> GetRevenueByMonthAsync(DateTime fromDate, DateTime toDate);
}
