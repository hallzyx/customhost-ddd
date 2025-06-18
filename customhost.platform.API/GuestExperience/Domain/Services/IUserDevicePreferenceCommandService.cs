using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Commands;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// User Device Preference command service interface
/// </summary>
public interface IUserDevicePreferenceCommandService
{
    Task<UserDevicePreference?> Handle(CreateUserDevicePreferenceCommand command);
    Task<UserDevicePreference?> Handle(UpdateUserDevicePreferenceCommand command);
    Task<bool> Handle(DeleteUserDevicePreferenceCommand command);
}
