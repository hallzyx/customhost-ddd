namespace customhost_backend.analytics.Interfaces.REST.Resources;

/// <summary>
/// Resource for rooms occupancy status response
/// </summary>
public record RoomsOccupancyStatusResource(
    int TotalRooms,
    int OccupiedRooms,
    int AvailableRooms,
    double OccupancyRate,
    int MaintenanceRooms,
    DateTime LastUpdated
);
