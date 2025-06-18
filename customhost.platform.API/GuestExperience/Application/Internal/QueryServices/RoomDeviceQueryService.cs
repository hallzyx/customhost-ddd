using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;
using customhost.platform.API.GuestExperience.Domain.Repositories;
using customhost.platform.API.GuestExperience.Domain.Services;

namespace customhost.platform.API.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// Room Device query service implementation
/// </summary>
public class RoomDeviceQueryService(IRoomDeviceRepository roomDeviceRepository) : IRoomDeviceQueryService
{
    public async Task<IEnumerable<RoomDevice>> Handle(GetAllRoomDevicesQuery query)
    {
        return await roomDeviceRepository.ListAsync();
    }

    public async Task<RoomDevice?> Handle(GetRoomDeviceByIdQuery query)
    {
        return await roomDeviceRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByRoomIdQuery query)
    {
        return await roomDeviceRepository.FindByRoomIdAsync(query.RoomId);
    }
}
