using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Repositories;

public class RoomRepository(AppDbContext context)
: BaseRepository<Room>(context), IRoomRespository
{
    public async Task<Room?> FindRoomsByRoomNameAsync(int roomName)
    {
        return await Context.Set<Room>().FirstOrDefaultAsync(r => r.RoomNumber == roomName);
    }
}