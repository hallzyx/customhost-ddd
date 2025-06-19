using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Repositories;

public class ServiceRequestRepository(AppDbContext context )
: BaseRepository<ServiceRequest>(context),IServiceRequestRepository
{
    public async Task<IEnumerable<ServiceRequest>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<ServiceRequest>()
            .Where(sr => sr.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<ServiceRequest>()
            .Where(sr => sr.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> FindByRoomIdAsync(int roomId)
    {
        return await Context.Set<ServiceRequest>()
            .Where(sr => sr.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> FindByStatusAsync(EServiceRequestStatus status)
    {
        return await Context.Set<ServiceRequest>()
            .Where(sr => sr.Status == status)
            .ToListAsync();
    }
}