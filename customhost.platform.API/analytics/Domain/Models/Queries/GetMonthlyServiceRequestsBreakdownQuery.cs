using System.ComponentModel.DataAnnotations;

namespace customhost_backend.analytics.Domain.Models.Queries;

/// <summary>
/// Query to get monthly service requests breakdown by type
/// </summary>
public record GetMonthlyServiceRequestsBreakdownQuery(
    [Required] int Months = 6
);
