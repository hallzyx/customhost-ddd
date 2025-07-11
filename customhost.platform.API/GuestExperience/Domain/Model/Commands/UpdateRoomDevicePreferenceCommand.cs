using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update a Room Device Preference
/// </summary>
public record UpdateRoomDevicePreferenceCommand(
    [Required] int Id,
    [Required] int RoomDeviceId,
    [Required] string Preferences
);
