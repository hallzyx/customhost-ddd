using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Commands;

/// <summary>
/// Command to delete an IoT Device
/// </summary>
public record DeleteIoTDeviceCommand(
    [Required] int Id
);
