using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device Preference command service interface
/// </summary>
public interface IRoomDevicePreferenceCommandService
{
    Task<RoomDevicePreference?> Handle(CreateRoomDevicePreferenceCommand command);
    Task<RoomDevicePreference?> Handle(UpdateRoomDevicePreferenceCommand command);
}
