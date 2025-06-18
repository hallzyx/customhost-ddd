using customhost_backend.analytics.Domain.Models.Queries;
using customhost_backend.analytics.Domain.Services;
using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.analytics.Interfaces.REST.Resources;
using System.Text.Json;

namespace customhost_backend.analytics.Application.Internal.QueryServices;

/// <summary>
/// Analytics snapshot query service implementation
/// </summary>
public class AnalyticsSnapshotQueryService(
    IGuestExperienceContextFacade guestExperienceContextFacade,
    ICrmContextFacade crmContextFacade
) : IAnalyticsSnapshotQueryService
{
    public async Task<IoTDevicesOnlineStatusResource> Handle(GetIoTDevicesOnlineStatusQuery query)
    {
        var totalDevices = await guestExperienceContextFacade.GetTotalDevicesCountAsync();
        var onlineDevices = await guestExperienceContextFacade.GetOnlineDevicesCountAsync();
        var offlineDevices = totalDevices - onlineDevices;
        var onlinePercentage = totalDevices > 0 ? Math.Round((double)onlineDevices / totalDevices * 100, 2) : 0;

        return new IoTDevicesOnlineStatusResource(
            totalDevices,
            onlineDevices,
            offlineDevices,
            onlinePercentage,
            DateTime.UtcNow
        );
    }

    public async Task<RoomsOccupancyStatusResource> Handle(GetRoomsOccupancyStatusQuery query)
    {
        var totalRooms = await crmContextFacade.GetTotalRoomsCountAsync();
        var occupiedRooms = await crmContextFacade.GetOccupiedRoomsCountAsync();
        var availableRooms = await crmContextFacade.GetAvailableRoomsCountAsync();
        var maintenanceRooms = await crmContextFacade.GetMaintenanceRoomsCountAsync();
        var occupancyRate = totalRooms > 0 ? Math.Round((double)occupiedRooms / totalRooms * 100, 2) : 0;

        return new RoomsOccupancyStatusResource(
            totalRooms,
            occupiedRooms,
            availableRooms,
            occupancyRate,
            maintenanceRooms,
            DateTime.UtcNow
        );
    }
}
