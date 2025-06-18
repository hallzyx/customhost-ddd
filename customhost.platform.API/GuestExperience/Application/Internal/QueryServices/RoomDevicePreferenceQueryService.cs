using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;
using customhost.platform.API.GuestExperience.Domain.Repositories;
using customhost.platform.API.GuestExperience.Domain.Services;

namespace customhost.platform.API.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// Room Device Preference query service implementation
/// </summary>
public class RoomDevicePreferenceQueryService(IRoomDevicePreferenceRepository roomDevicePreferenceRepository) : IRoomDevicePreferenceQueryService
{
    public async Task<IEnumerable<RoomDevicePreference>> Handle(GetAllRoomDevicePreferencesQuery query)
    {
        return await roomDevicePreferenceRepository.ListAsync();
    }

    public async Task<RoomDevicePreference?> Handle(GetRoomDevicePreferenceByIdQuery query)
    {
        return await roomDevicePreferenceRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<RoomDevicePreference>> Handle(GetRoomDevicePreferencesByRoomDeviceIdQuery query)
    {
        return await roomDevicePreferenceRepository.FindByRoomDeviceIdAsync(query.RoomDeviceId);
    }
}
