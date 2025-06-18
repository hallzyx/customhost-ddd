using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update a User Device Preference
/// </summary>
public record UpdateUserDevicePreferenceCommand(
    [Required] int Id,
    int? UserId,
    [Required] int DeviceId,
    [Required] string CustomName,
    [Required] string Overrides
);
