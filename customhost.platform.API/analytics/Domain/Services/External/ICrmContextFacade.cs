using customhost_backend.analytics.Domain.Models.ValueObjects;

namespace customhost_backend.analytics.Domain.Services.External;

/// <summary>
/// Anti-Corruption Layer facade for accessing CRM context data
/// </summary>
public interface ICrmContextFacade
{
    Task<int> GetTotalRoomsCountAsync();
    Task<int> GetOccupiedRoomsCountAsync();
    Task<int> GetAvailableRoomsCountAsync();
    Task<int> GetMaintenanceRoomsCountAsync();
    Task<Dictionary<string, int>> GetServiceRequestsCountByTypeAsync(DateTime fromDate, DateTime toDate);
    Task<Dictionary<string, int>> GetServiceRequestsCountByTypeForMonthAsync(int year, int month);
}
