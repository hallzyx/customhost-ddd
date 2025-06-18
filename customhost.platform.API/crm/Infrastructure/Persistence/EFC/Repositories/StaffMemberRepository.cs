using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Persistence.EFC.Repositories;

public class StaffMemberRepository(AppDbContext context) : BaseRepository<StaffMember>(context), IStaffMemberRepository
{
    public async Task<IEnumerable<StaffMember>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<StaffMember>()
            .Where(s => s.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<StaffMember>> FindByDepartmentAsync(int hotelId, string department)
    {
        return await Context.Set<StaffMember>()
            .Where(s => s.HotelId == hotelId && s.Department.ToString() == department)
            .ToListAsync();
    }

    public async Task<IEnumerable<StaffMember>> FindActiveByHotelIdAsync(int hotelId)
    {
        return await Context.Set<StaffMember>()
            .Where(s => s.HotelId == hotelId && s.IsActive)
            .ToListAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Set<StaffMember>()
            .AnyAsync(s => s.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> ExistsByEmailAndIdNotAsync(string email, int id)
    {
        return await Context.Set<StaffMember>()
            .AnyAsync(s => s.Email.ToLower() == email.ToLower() && s.Id != id);
    }
}
