using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Room Command Service Interface
/// </summary>
public interface IRoomCommandService
{
    /// <summary>
    /// Handle create room command
    /// </summary>
    /// <param name="command">Create room command</param>
    /// <returns>Created room or null if failed</returns>
    Task<Room?> Handle(CreateRoomCommand command);
    
    /// <summary>
    /// Handle update room command
    /// </summary>
    /// <param name="command">Update room command</param>
    /// <returns>Updated room or null if failed</returns>
    Task<Room?> Handle(UpdateRoomCommand command);
    
    /// <summary>
    /// Handle delete room command
    /// </summary>
    /// <param name="command">Delete room command</param>
    /// <returns>True if deleted, false otherwise</returns>
    Task<bool> Handle(DeleteRoomCommand command);
}