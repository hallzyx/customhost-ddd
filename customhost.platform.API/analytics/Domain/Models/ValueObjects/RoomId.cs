namespace customhost_backend.analytics.Domain.Models.ValueObjects;

/// <summary>
/// Represents the identifier for a room from the CRM bounded context.
/// This follows DDD principles for cross-bounded context communication.
/// </summary>
/// <param name="Id">The room identifier</param>
public record RoomId(int Id);
