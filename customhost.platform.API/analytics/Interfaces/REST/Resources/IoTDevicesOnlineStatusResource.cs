namespace customhost_backend.analytics.Interfaces.REST.Resources;

/// <summary>
/// Resource for IoT devices online status response
/// </summary>
public record IoTDevicesOnlineStatusResource(
    int TotalDevices,
    int OnlineDevices,
    int OfflineDevices,
    double OnlinePercentage,
    DateTime LastUpdated
);
