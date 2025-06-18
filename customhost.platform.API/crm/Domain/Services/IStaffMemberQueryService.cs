using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;

namespace customhost_backend.crm.Domain.Services;

public interface IStaffMemberQueryService
{
    Task<IEnumerable<StaffMember>> Handle(GetAllStaffMembersQuery query);
    Task<StaffMember?> Handle(GetStaffMemberByIdQuery query);
    Task<IEnumerable<StaffMember>> Handle(GetStaffMembersByHotelIdQuery query);
    Task<IEnumerable<StaffMember>> Handle(GetStaffMembersByDepartmentQuery query);
    Task<IEnumerable<StaffMember>> Handle(GetActiveStaffMembersQuery query);
}
