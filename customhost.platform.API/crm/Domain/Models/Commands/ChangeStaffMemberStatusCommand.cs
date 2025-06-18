using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

public record ChangeStaffMemberStatusCommand(int Id, StaffStatus Status);
