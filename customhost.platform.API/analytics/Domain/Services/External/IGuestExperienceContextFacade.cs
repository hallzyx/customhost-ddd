namespace customhost_backend.analytics.Domain.Services.External;

/// <summary>
/// Anti-Corruption Layer facade for accessing GuestExperience context data
/// </summary>
public interface IGuestExperienceContextFacade
{
    Task<int> GetTotalDevicesCountAsync();
    Task<int> GetOnlineDevicesCountAsync();
    Task<Dictionary<int, bool>> GetDevicesOnlineStatusAsync();
}
