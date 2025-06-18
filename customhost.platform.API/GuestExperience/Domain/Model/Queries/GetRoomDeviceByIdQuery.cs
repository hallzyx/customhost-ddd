using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get a Room Device by Id
/// </summary>
public record GetRoomDeviceByIdQuery(
    [Required] int Id
);
