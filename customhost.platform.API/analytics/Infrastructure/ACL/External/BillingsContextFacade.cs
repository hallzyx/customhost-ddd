using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.billings.Domain.Repositories;

namespace customhost_backend.analytics.Infrastructure.ACL.External;

/// <summary>
/// Anti-Corruption Layer implementation for Billings context
/// </summary>
public class BillingsContextFacade(
    IPaymentRepository paymentRepository
) : IBillingsContextFacade
{    public async Task<decimal> GetTotalRevenueForMonthAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var allPayments = await paymentRepository.ListAsync();
        var payments = allPayments.Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate);
        return payments
            .Where(p => p.Status.ToString() == "Completed" || p.Status.ToString() == "Paid")
            .Sum(p => p.Amount);
    }

    public async Task<int> GetBookingsCountForMonthAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        
        var allPayments = await paymentRepository.ListAsync();
        var payments = allPayments.Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate);
        return payments
            .Where(p => p.Status.ToString() == "Completed" || p.Status.ToString() == "Paid")
            .Count();
    }

    public async Task<Dictionary<string, decimal>> GetRevenueByMonthAsync(DateTime fromDate, DateTime toDate)
    {
        var allPayments = await paymentRepository.ListAsync();
        var payments = allPayments.Where(p => p.CreatedAt >= fromDate && p.CreatedAt <= toDate);
        return payments
            .Where(p => p.Status.ToString() == "Completed" || p.Status.ToString() == "Paid")
            .GroupBy(p => p.CreatedAt.ToString("yyyy-MM"))
            .ToDictionary(g => g.Key, g => g.Sum(p => p.Amount));
    }
}
