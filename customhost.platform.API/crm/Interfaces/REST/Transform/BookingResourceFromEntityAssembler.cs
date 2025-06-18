using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform booking entity to booking resource
/// </summary>
public static class BookingResourceFromEntityAssembler
{
    /// <summary>
    /// Transform booking entity to booking resource
    /// </summary>
    /// <param name="entity">Booking entity</param>
    /// <returns>Booking resource</returns>
    public static BookingResource ToResourceFromEntity(Booking entity)
    {
        return new BookingResource(
            entity.Id,
            entity.UserId,
            entity.HotelId,
            entity.RoomId,
            entity.CheckInDate,
            entity.CheckOutDate,
            entity.Status.ToString().ToLowerInvariant(),
            entity.TotalPrice,
            entity.PaymentStatus.ToString().ToLowerInvariant(),
            entity.SpecialRequests,
            entity.CreatedAt,
            entity.NumberOfNights,
            entity.IsActive,
            entity.IsCurrentlyCheckedIn,
            entity.Preferences,
            entity.AppliedDevicePreferences
        );
    }
}
