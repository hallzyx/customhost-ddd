using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform update booking resource to update booking command
/// </summary>
public static class UpdateBookingCommandFromResourceAssembler
{
    /// <summary>
    /// Transform update booking resource to update booking command
    /// </summary>
    /// <param name="id">Booking ID</param>
    /// <param name="resource">Update booking resource</param>
    /// <returns>Update booking command</returns>
    public static UpdateBookingCommand ToCommandFromResource(int id, UpdateBookingResource resource)
    {
        return new UpdateBookingCommand(
            id,
            resource.CheckInDate,
            resource.CheckOutDate,
            resource.TotalPrice,
            resource.SpecialRequests
        );
    }
}
