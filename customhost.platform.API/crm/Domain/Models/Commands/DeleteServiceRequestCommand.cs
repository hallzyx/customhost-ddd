namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Delete Service Request Command
/// </summary>
/// <param name="Id">Service request ID</param>
public record DeleteServiceRequestCommand(int Id);
