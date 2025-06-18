using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get a User Device Preference by Id
/// </summary>
public record GetUserDevicePreferenceByIdQuery(
    [Required] int Id
);
