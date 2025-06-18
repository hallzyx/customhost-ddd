using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get Room Devices by Room Id
/// </summary>
public record GetRoomDevicesByRoomIdQuery(
    [Required] int RoomId
);
