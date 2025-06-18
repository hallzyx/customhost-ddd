using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform create booking resource to create booking command
/// </summary>
public static class CreateBookingCommandFromResourceAssembler
{
    /// <summary>
    /// Transform create booking resource to create booking command
    /// </summary>
    /// <param name="resource">Create booking resource</param>
    /// <returns>Create booking command</returns>
    public static CreateBookingCommand ToCommandFromResource(CreateBookingResource resource)
    {
        return new CreateBookingCommand(
            resource.UserId,
            resource.HotelId,
            resource.RoomId,
            resource.CheckInDate,
            resource.CheckOutDate,
            resource.TotalPrice,
            resource.SpecialRequests,
            resource.Preferences,
            resource.AppliedDevicePreferences
        );
    }
}
