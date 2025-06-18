using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Room Device Preference entity configuration for Entity Framework Core
/// </summary>
public class RoomDevicePreferenceConfiguration : IEntityTypeConfiguration<RoomDevicePreference>
{
    public void Configure(EntityTypeBuilder<RoomDevicePreference> builder)
    {
        builder.ToTable("room_device_preferences");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.RoomDeviceId)
               .HasColumnName("room_device_id")
               .IsRequired();
               
        builder.Property(e => e.Preferences)
               .HasColumnName("preferences")
               .HasColumnType("text")
               .IsRequired();
               
        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at");
        
        // Relationship with RoomDevice
        builder.HasOne(e => e.RoomDevice)
               .WithMany()
               .HasForeignKey(e => e.RoomDeviceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
