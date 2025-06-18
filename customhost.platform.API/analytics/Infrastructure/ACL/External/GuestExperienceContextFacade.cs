using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.GuestExperience.Domain.Repositories;

namespace customhost_backend.analytics.Infrastructure.ACL.External;

/// <summary>
/// Anti-Corruption Layer implementation for GuestExperience context
/// </summary>
public class GuestExperienceContextFacade(
    IIoTDeviceRepository iotDeviceRepository
) : IGuestExperienceContextFacade
{
    public async Task<int> GetTotalDevicesCountAsync()
    {
        var devices = await iotDeviceRepository.ListAsync();
        return devices.Count();
    }

    public async Task<int> GetOnlineDevicesCountAsync()
    {
        var devices = await iotDeviceRepository.ListAsync();
        // Assuming IoTDevice has an IsOnline property or similar status
        return devices.Count(d => d.Status == "Online" || d.Status == "Active");
    }

    public async Task<Dictionary<int, bool>> GetDevicesOnlineStatusAsync()
    {
        var devices = await iotDeviceRepository.ListAsync();
        return devices.ToDictionary(
            d => d.Id, 
            d => d.Status == "Online" || d.Status == "Active"
        );
    }
}
