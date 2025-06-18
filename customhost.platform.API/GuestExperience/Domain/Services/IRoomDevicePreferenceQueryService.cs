using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device Preference query service interface
/// </summary>
public interface IRoomDevicePreferenceQueryService
{
    Task<IEnumerable<RoomDevicePreference>> Handle(GetAllRoomDevicePreferencesQuery query);
    Task<RoomDevicePreference?> Handle(GetRoomDevicePreferenceByIdQuery query);
    Task<IEnumerable<RoomDevicePreference>> Handle(GetRoomDevicePreferencesByRoomDeviceIdQuery query);
}
