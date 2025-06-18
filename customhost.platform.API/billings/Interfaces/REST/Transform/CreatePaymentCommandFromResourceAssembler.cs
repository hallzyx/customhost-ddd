using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
    {
        if (!Enum.TryParse<EPaymentMethod>(resource.PaymentMethod, true, out var paymentMethod))
            paymentMethod = EPaymentMethod.CreditCard;

        if (!Enum.TryParse<EPaymentStatus>(resource.Status, true, out var status))
            status = EPaymentStatus.Pending;

        return new CreatePaymentCommand(
            resource.BookingId,
            resource.UserId ?? 0,
            resource.HotelId ?? 0,
            resource.RoomId ?? 0,
            resource.Amount ?? 0,
            resource.Currency ?? "USD",
            resource.CheckInDate,
            resource.CheckOutDate,
            paymentMethod,
            status
        );
    }
}
