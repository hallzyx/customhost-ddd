using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to update an existing IoT device
/// </summary>
/// <param name="Id">The ID of the device to update</param>
/// <param name="Name">The new name of the device (required, max 100 characters)</param>
/// <param name="DeviceType">The new type of device (required, max 50 characters)</param>
/// <param name="ConfigSchema">New JSON schema for device configuration</param>
public record UpdateIoTDeviceCommand(
    [Range(1, int.MaxValue)] int Id,
    [Required][StringLength(100, MinimumLength = 1)] string Name,
    [Required][StringLength(50, MinimumLength = 1)] string DeviceType,
    string? ConfigSchema
);
