using System.ComponentModel.DataAnnotations;

namespace customhost_backend.analytics.Domain.Models.Aggregates;

/// <summary>
/// Metric Data aggregate representing historical metrics calculated over time periods
/// </summary>
public class MetricData
{
    [Key] public int Id { get; private set; }
    
    [Required] [MaxLength(100)] public string MetricType { get; private set; }
    
    [Required] public DateTime PeriodStart { get; private set; }
    
    [Required] public DateTime PeriodEnd { get; private set; }
    
    [Required] public string Data { get; private set; } // JSON data
    
    [Required] public DateTime CalculatedAt { get; private set; }

    // Parameterless constructor for EF Core
    protected MetricData() { }

    public MetricData(string metricType, DateTime periodStart, DateTime periodEnd, string data)
    {
        MetricType = metricType ?? throw new ArgumentNullException(nameof(metricType));
        Data = data ?? throw new ArgumentNullException(nameof(data));
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        CalculatedAt = DateTime.UtcNow;
    }

    public bool IsForPeriod(DateTime start, DateTime end)
    {
        return PeriodStart.Date == start.Date && PeriodEnd.Date == end.Date;
    }
}
