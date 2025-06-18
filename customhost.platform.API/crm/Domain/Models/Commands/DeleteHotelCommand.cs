namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Delete Hotel Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the hotel to delete.
/// </param>
public record DeleteHotelCommand(int Id);
