using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// Room Device query service interface
/// </summary>
public interface IRoomDeviceQueryService
{
    Task<IEnumerable<RoomDevice>> Handle(GetAllRoomDevicesQuery query);
    Task<RoomDevice?> Handle(GetRoomDeviceByIdQuery query);
    Task<IEnumerable<RoomDevice>> Handle(GetRoomDevicesByRoomIdQuery query);
}
