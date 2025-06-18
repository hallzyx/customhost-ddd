namespace customhost_backend.analytics.Domain.Models.ValueObjects;

/// <summary>
/// Represents the identifier for a payment from the Billings bounded context.
/// This follows DDD principles for cross-bounded context communication.
/// </summary>
/// <param name="Id">The payment identifier</param>
public record PaymentId(int Id);
