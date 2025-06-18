using System.ComponentModel.DataAnnotations;

namespace customhost_backend.analytics.Domain.Models.Queries;

/// <summary>
/// Query to get monthly revenue trend metrics
/// </summary>
public record GetMonthlyRevenueTrendQuery(
    [Required] int Months = 6
);
