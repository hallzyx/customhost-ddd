using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Commands;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// Room Device Preference command service interface
/// </summary>
public interface IRoomDevicePreferenceCommandService
{
    Task<RoomDevicePreference?> Handle(CreateRoomDevicePreferenceCommand command);
    Task<RoomDevicePreference?> Handle(UpdateRoomDevicePreferenceCommand command);
}
