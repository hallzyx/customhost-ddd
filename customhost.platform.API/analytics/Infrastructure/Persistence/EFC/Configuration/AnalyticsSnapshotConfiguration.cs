using customhost_backend.analytics.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.analytics.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// AnalyticsSnapshot entity configuration for Entity Framework Core
/// </summary>
public class AnalyticsSnapshotConfiguration : IEntityTypeConfiguration<AnalyticsSnapshot>
{
    public void Configure(EntityTypeBuilder<AnalyticsSnapshot> builder)
    {
        builder.ToTable("analytics_snapshots");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.Timestamp)
               .HasColumnName("timestamp")
               .IsRequired();
               
        builder.Property(e => e.SnapshotType)
               .HasColumnName("snapshot_type")
               .HasMaxLength(100)
               .IsRequired();
               
        builder.Property(e => e.Data)
               .HasColumnName("data")
               .HasColumnType("text")
               .IsRequired();
               
        builder.Property(e => e.ExpiresAt)
               .HasColumnName("expires_at")
               .IsRequired();
               
        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        // Index for quick lookup by type
        builder.HasIndex(e => e.SnapshotType);
        
        // Index for expiration cleanup
        builder.HasIndex(e => e.ExpiresAt);
    }
}
