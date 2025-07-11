using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to create a new Room Device
/// </summary>
public record CreateRoomDeviceCommand(
    [Required] int RoomId,
    [Required] int IoTDeviceId,
    [Required] string Status = "working"
);
