using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// User Device Preference entity configuration for Entity Framework Core
/// </summary>
public class UserDevicePreferenceConfiguration : IEntityTypeConfiguration<UserDevicePreference>
{
    public void Configure(EntityTypeBuilder<UserDevicePreference> builder)
    {
        builder.ToTable("user_device_preferences");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.UserId)
               .HasColumnName("user_id");
               
        builder.Property(e => e.DeviceId)
               .HasColumnName("device_id")
               .IsRequired();
               
        builder.Property(e => e.CustomName)
               .HasColumnName("custom_name")
               .HasMaxLength(100)
               .IsRequired();
               
        builder.Property(e => e.Overrides)
               .HasColumnName("overrides")
               .HasColumnType("text")
               .IsRequired();
               
        builder.Property(e => e.LastUpdated)
               .HasColumnName("last_updated");
        
        // Relationship with IoTDevice
        builder.HasOne(e => e.Device)
               .WithMany()
               .HasForeignKey(e => e.DeviceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
