using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to delete a Room Device Preference
/// </summary>
public record DeleteRoomDevicePreferenceCommand(
    [Required] int Id
);
