namespace customhost_backend.analytics.Domain.Models.ValueObjects;

/// <summary>
/// Represents the identifier for a device from the GuestExperience bounded context.
/// This follows DDD principles for cross-bounded context communication.
/// </summary>
/// <param name="Id">The device identifier</param>
public record DeviceId(int Id);
