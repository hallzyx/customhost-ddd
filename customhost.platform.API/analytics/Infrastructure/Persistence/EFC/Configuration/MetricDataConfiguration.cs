using customhost_backend.analytics.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace customhost_backend.analytics.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// MetricData entity configuration for Entity Framework Core
/// </summary>
public class MetricDataConfiguration : IEntityTypeConfiguration<MetricData>
{
    public void Configure(EntityTypeBuilder<MetricData> builder)
    {
        builder.ToTable("metric_data");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        builder.Property(e => e.MetricType)
               .HasColumnName("metric_type")
               .HasMaxLength(100)
               .IsRequired();
               
        builder.Property(e => e.PeriodStart)
               .HasColumnName("period_start")
               .IsRequired();
               
        builder.Property(e => e.PeriodEnd)
               .HasColumnName("period_end")
               .IsRequired();
               
        builder.Property(e => e.Data)
               .HasColumnName("data")
               .HasColumnType("text")
               .IsRequired();
               
        builder.Property(e => e.CalculatedAt)
               .HasColumnName("calculated_at")
               .IsRequired();

        // Index for quick lookup by metric type and period
        builder.HasIndex(e => new { e.MetricType, e.PeriodStart, e.PeriodEnd });
        
        // Index for metric type queries
        builder.HasIndex(e => e.MetricType);
    }
}
