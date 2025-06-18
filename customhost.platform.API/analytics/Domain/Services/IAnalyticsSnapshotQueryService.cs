using customhost_backend.analytics.Domain.Models.Queries;
using customhost_backend.analytics.Interfaces.REST.Resources;

namespace customhost_backend.analytics.Domain.Services;

/// <summary>
/// Analytics query service interface for snapshot-based real-time data
/// </summary>
public interface IAnalyticsSnapshotQueryService
{
    Task<IoTDevicesOnlineStatusResource> Handle(GetIoTDevicesOnlineStatusQuery query);
    Task<RoomsOccupancyStatusResource> Handle(GetRoomsOccupancyStatusQuery query);
}
