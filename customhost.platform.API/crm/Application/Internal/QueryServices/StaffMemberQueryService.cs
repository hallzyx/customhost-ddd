using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

public class StaffMemberQueryService(IStaffMemberRepository staffMemberRepository) : IStaffMemberQueryService
{
    public async Task<IEnumerable<StaffMember>> Handle(GetAllStaffMembersQuery query)
    {
        return await staffMemberRepository.ListAsync();
    }

    public async Task<StaffMember?> Handle(GetStaffMemberByIdQuery query)
    {
        return await staffMemberRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<StaffMember>> Handle(GetStaffMembersByHotelIdQuery query)
    {
        return await staffMemberRepository.FindByHotelIdAsync(query.HotelId);
    }

    public async Task<IEnumerable<StaffMember>> Handle(GetStaffMembersByDepartmentQuery query)
    {
        return await staffMemberRepository.FindByDepartmentAsync(query.HotelId, query.Department);
    }

    public async Task<IEnumerable<StaffMember>> Handle(GetActiveStaffMembersQuery query)
    {
        return await staffMemberRepository.FindActiveByHotelIdAsync(query.HotelId);
    }
}
