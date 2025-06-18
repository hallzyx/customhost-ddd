namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Resolve Service Request Command
/// </summary>
/// <param name="ServiceRequestId">Service request ID</param>
public record ResolveServiceRequestCommand(int ServiceRequestId);
