using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

public class RoomCommandService
(IRoomRespository roomRepository, IUnitOfWork unitOfWork)
: IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        
        var certainRoom = await roomRepository.FindRoomsByRoomNameAsync(command.RoomNumber);

        if (certainRoom != null)
        {
            return null;
        }
        var room = new Room(command);
        await roomRepository.AddAsync(room);
        await unitOfWork.CompleteAsync();
        return room;
        }
    
}

