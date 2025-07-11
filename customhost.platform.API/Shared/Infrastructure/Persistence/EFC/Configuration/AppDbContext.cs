using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.crm.Infrastructure.Persistence.EFC.Configuration.Extensions;
using customhost_backend.Shared.Infrastructure.Interfaces.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Guest Experience DbSets
    public DbSet<IoTDevice> IoTDevices { get; set; }
    public DbSet<RoomDevice> RoomDevices { get; set; }
    public DbSet<RoomDevicePreference> RoomDevicePreferences { get; set; }
    public DbSet<UserDevicePreference> UserDevicePreferences { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        
        builder.ApplyCrmConfiguration();
        builder.ApplyGuestExperienceConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}