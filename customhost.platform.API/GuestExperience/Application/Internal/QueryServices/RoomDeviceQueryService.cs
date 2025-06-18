using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;

namespace customhost_backend.GuestExperience.Application.Internal.QueryServices;

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
