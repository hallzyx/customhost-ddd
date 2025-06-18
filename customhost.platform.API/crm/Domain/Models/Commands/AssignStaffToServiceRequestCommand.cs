namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Assign Staff To Service Request Command
/// </summary>
/// <param name="ServiceRequestId">Service request ID</param>
/// <param name="StaffId">Staff member ID</param>
public record AssignStaffToServiceRequestCommand(int ServiceRequestId, int StaffId);
