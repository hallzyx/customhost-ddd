using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to create a User Device Preference
/// </summary>
public record CreateUserDevicePreferenceCommand(
    int? UserId,
    [Required] int DeviceId,
    [Required] string CustomName,
    [Required] string Overrides
);
