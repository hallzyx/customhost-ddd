using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get Room Devices by Room Id
/// </summary>
public record GetRoomDevicesByRoomIdQuery(
    [Required] int RoomId
);
