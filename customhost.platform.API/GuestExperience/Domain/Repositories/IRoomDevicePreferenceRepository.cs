using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for Room Device Preference aggregate
/// </summary>
public interface IRoomDevicePreferenceRepository : IBaseRepository<RoomDevicePreference>
{
    Task<IEnumerable<RoomDevicePreference>> FindByRoomDeviceIdAsync(int roomDeviceId);
    Task<RoomDevicePreference?> FindByRoomDeviceIdSingleAsync(int roomDeviceId);
}
