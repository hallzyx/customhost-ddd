using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to create a new IoT device
/// </summary>
/// <param name="Name">The name of the device (required, max 100 characters)</param>
/// <param name="DeviceType">The type of device (required, max 50 characters)</param>
/// <param name="ConfigSchema">JSON schema for device configuration (optional, defaults to "{}")</param>
public record CreateIoTDeviceCommand(
    [Required][StringLength(100, MinimumLength = 1)] string Name,
    [Required][StringLength(50, MinimumLength = 1)] string DeviceType,
    string? ConfigSchema
);
