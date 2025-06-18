namespace customhost_backend.analytics.Interfaces.REST.Resources;

/// <summary>
/// Resource for monthly service requests breakdown response
/// </summary>
public record MonthlyServiceRequestsBreakdownResource(
    string Period,
    IEnumerable<MonthlyServiceRequestDataResource> Data
);

/// <summary>
/// Resource for individual month service request data
/// </summary>
public record MonthlyServiceRequestDataResource(
    string Month,
    Dictionary<string, int> Breakdown,
    int Total
);
