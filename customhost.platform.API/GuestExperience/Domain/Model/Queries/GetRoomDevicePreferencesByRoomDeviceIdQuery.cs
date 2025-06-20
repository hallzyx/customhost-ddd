using System.ComponentModel.DataAnnotations;

namespace customhost.platform.API.GuestExperience.Domain.Model.Queries;

/// <summary>
/// Query to get Room Device Preferences by Room Device Id
/// </summary>
public record GetRoomDevicePreferencesByRoomDeviceIdQuery(
    [Required] int RoomDeviceId
);
