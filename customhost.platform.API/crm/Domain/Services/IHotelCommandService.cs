using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Hotel command service interface
/// </summary>
public interface IHotelCommandService
{
    /// <summary>
    /// Handle create hotel command
    /// </summary>
    /// <param name="command">Create hotel command</param>
    /// <returns>Created hotel or null if failed</returns>
    Task<Hotel?> Handle(CreateHotelCommand command);
    
    /// <summary>
    /// Handle update hotel command
    /// </summary>
    /// <param name="command">Update hotel command</param>
    /// <returns>Updated hotel or null if failed</returns>
    Task<Hotel?> Handle(UpdateHotelCommand command);
    
    /// <summary>
    /// Handle delete hotel command
    /// </summary>
    /// <param name="command">Delete hotel command</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> Handle(DeleteHotelCommand command);
}
