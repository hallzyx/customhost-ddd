using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to delete a Room Device
/// </summary>
public record DeleteRoomDeviceCommand(
    [Required] int Id
);
