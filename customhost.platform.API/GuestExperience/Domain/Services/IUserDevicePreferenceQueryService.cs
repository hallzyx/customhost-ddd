using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// User Device Preference query service interface
/// </summary>
public interface IUserDevicePreferenceQueryService
{
    Task<IEnumerable<UserDevicePreference>> Handle(GetAllUserDevicePreferencesQuery query);
    Task<UserDevicePreference?> Handle(GetUserDevicePreferenceByIdQuery query);
    Task<IEnumerable<UserDevicePreference>> Handle(GetUserDevicePreferencesByUserIdQuery query);
}
