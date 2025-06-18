using System.ComponentModel.DataAnnotations;

namespace customhost_backend.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get an IoT Device by Id
/// </summary>
public record GetIoTDeviceByIdQuery(
    [Required] int Id
);
