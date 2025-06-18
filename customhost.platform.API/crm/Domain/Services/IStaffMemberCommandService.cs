using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

public interface IStaffMemberCommandService
{
    Task<StaffMember?> Handle(CreateStaffMemberCommand command);
    Task<StaffMember?> Handle(UpdateStaffMemberCommand command);
    Task<bool> Handle(DeleteStaffMemberCommand command);
    Task<StaffMember?> Handle(ChangeStaffMemberStatusCommand command);
}
