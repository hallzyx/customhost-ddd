using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.crm.Domain.Repositories;

namespace customhost_backend.analytics.Infrastructure.ACL.External;

/// <summary>
/// Anti-Corruption Layer implementation for CRM context
/// </summary>
public class CrmContextFacade(
    IRoomRepository roomRepository,
    IServiceRequestRepository serviceRequestRepository
) : ICrmContextFacade
{
    public async Task<int> GetTotalRoomsCountAsync()
    {
        var rooms = await roomRepository.ListAsync();
        return rooms.Count();
    }

    public async Task<int> GetOccupiedRoomsCountAsync()
    {
        var rooms = await roomRepository.ListAsync();
        return rooms.Count(r => r.Status.ToString() == "Occupied");
    }

    public async Task<int> GetAvailableRoomsCountAsync()
    {
        var rooms = await roomRepository.ListAsync();
        return rooms.Count(r => r.Status.ToString() == "Available");
    }

    public async Task<int> GetMaintenanceRoomsCountAsync()
    {
        var rooms = await roomRepository.ListAsync();
        return rooms.Count(r => r.Status.ToString() == "Maintenance");
    }    public async Task<Dictionary<string, int>> GetServiceRequestsCountByTypeAsync(DateTime fromDate, DateTime toDate)
    {
        var allRequests = await serviceRequestRepository.ListAsync();
        var requests = allRequests.Where(r => r.CreatedAt >= fromDate && r.CreatedAt <= toDate);
        return requests
            .GroupBy(r => r.Type.ToString())
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public async Task<Dictionary<string, int>> GetServiceRequestsCountByTypeForMonthAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        return await GetServiceRequestsCountByTypeAsync(startDate, endDate);
    }
}
