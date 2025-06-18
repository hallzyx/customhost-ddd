using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// IoT Device entity configuration for Entity Framework Core
/// </summary>
public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
{
    public void Configure(EntityTypeBuilder<IoTDevice> builder)
    {
        builder.ToTable("iot_devices");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name)
               .HasColumnName("name")
               .HasMaxLength(100)
               .IsRequired();
               
        builder.Property(e => e.DeviceType)
               .HasColumnName("device_type")
               .HasMaxLength(50)
               .IsRequired();
               
        builder.Property(e => e.ConfigSchema)
               .HasColumnName("config_schema")
               .HasColumnType("text");
               
        builder.Property(e => e.Status)
               .HasColumnName("status")
               .HasMaxLength(20)
               .IsRequired();
               
        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at");
    }
}
