using Microsoft.EntityFrameworkCore;

namespace customhost.platform.API.GuestExperience.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Guest Experience context extension methods for ModelBuilder
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Apply Guest Experience configuration to ModelBuilder
    /// </summary>
    /// <param name="builder">ModelBuilder instance</param>
    public static void ApplyGuestExperienceConfiguration(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new IoTDeviceConfiguration());
        builder.ApplyConfiguration(new RoomDeviceConfiguration());
        builder.ApplyConfiguration(new RoomDevicePreferenceConfiguration());
        builder.ApplyConfiguration(new UserDevicePreferenceConfiguration());
    }
}
