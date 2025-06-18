using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get User Device Preferences by User Id
/// </summary>
public record GetUserDevicePreferencesByUserIdQuery(
    int? UserId
);
