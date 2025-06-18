using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// User Device Preference command service interface
/// </summary>
public interface IUserDevicePreferenceCommandService
{
    Task<UserDevicePreference?> Handle(CreateUserDevicePreferenceCommand command);
    Task<UserDevicePreference?> Handle(UpdateUserDevicePreferenceCommand command);
    Task<bool> Handle(DeleteUserDevicePreferenceCommand command);
}
