using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update an existing Room Device
/// </summary>
public record UpdateRoomDeviceCommand(
    [Required] int Id,
    [Required] int RoomId,
    [Required] int IoTDeviceId,
    [Required] string Status
);
