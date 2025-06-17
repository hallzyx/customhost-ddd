using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

public interface IRoomRespository : IBaseRepository<Room>
{
    Task<Room?> FindRoomsByRoomNameAsync(int roomName);
    
}