using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

public interface IStaffMemberRepository : IBaseRepository<StaffMember>
{
    Task<IEnumerable<StaffMember>> FindByHotelIdAsync(int hotelId);
    Task<IEnumerable<StaffMember>> FindByDepartmentAsync(int hotelId, string department);
    Task<IEnumerable<StaffMember>> FindActiveByHotelIdAsync(int hotelId);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailAndIdNotAsync(string email, int id);
}
