using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to delete a User Device Preference
/// </summary>
public record DeleteUserDevicePreferenceCommand(
    [Required] int Id
);
