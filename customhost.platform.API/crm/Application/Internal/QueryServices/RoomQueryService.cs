using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.QueryServices;

public class RoomQueryService
(IRoomRespository roomRepository, IUnitOfWork unitOfWork)
: IRoomQueryService
{
    public Task<Room?> Handle(GetHotelByRoomNameQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Room>> Handle()
    {
        return roomRepository.ListAsync();
    }
}