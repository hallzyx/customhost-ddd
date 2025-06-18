using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;
using customhost.platform.API.GuestExperience.Domain.Repositories;
using customhost.platform.API.GuestExperience.Domain.Services;

namespace customhost.platform.API.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// User Device Preference query service implementation
/// </summary>
public class UserDevicePreferenceQueryService(IUserDevicePreferenceRepository userDevicePreferenceRepository) : IUserDevicePreferenceQueryService
{
    public async Task<IEnumerable<UserDevicePreference>> Handle(GetAllUserDevicePreferencesQuery query)
    {
        return await userDevicePreferenceRepository.ListAsync();
    }

    public async Task<UserDevicePreference?> Handle(GetUserDevicePreferenceByIdQuery query)
    {
        return await userDevicePreferenceRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<UserDevicePreference>> Handle(GetUserDevicePreferencesByUserIdQuery query)
    {
        return await userDevicePreferenceRepository.FindByUserIdAsync(query.UserId);
    }
}
