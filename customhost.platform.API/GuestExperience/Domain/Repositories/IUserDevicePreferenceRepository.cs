using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost.platform.API.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for User Device Preference aggregate
/// </summary>
public interface IUserDevicePreferenceRepository : IBaseRepository<UserDevicePreference>
{
    Task<IEnumerable<UserDevicePreference>> FindByUserIdAsync(int? userId);
    Task<IEnumerable<UserDevicePreference>> FindByDeviceIdAsync(int deviceId);
    Task<UserDevicePreference?> FindByUserIdAndDeviceIdAsync(int? userId, int deviceId);
}
