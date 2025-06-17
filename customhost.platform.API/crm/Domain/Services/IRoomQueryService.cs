using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;

namespace customhost_backend.crm.Domain.Services;

public interface IRoomQueryService
{
    Task<Room?> Handle(GetHotelByRoomNameQuery query);

    Task<IEnumerable<Room>> Handle();
}