namespace customhost_backend.analytics.Domain.Models.ValueObjects;

/// <summary>
/// Represents the identifier for a service request from the CRM bounded context.
/// This follows DDD principles for cross-bounded context communication.
/// </summary>
/// <param name="Id">The service request identifier</param>
public record ServiceRequestId(int Id);
