using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment payment)
    {
        return new PaymentResource(
            payment.Id,
            payment.BookingId,
            payment.UserId,
            payment.HotelId,
            payment.RoomId,
            payment.Amount,
            payment.Currency,
            payment.CheckInDate,
            payment.CheckOutDate,
            payment.PaymentMethod.ToString(),
            payment.Status.ToString(),
            payment.PaymentDate,
            payment.CreatedAt
        );
    }

    public static List<PaymentResource> ToResourcesFromEntities(IEnumerable<Payment> payments)
    {
        return payments.Select(payment => ToResourceFromEntity(payment)).ToList();
    }
}
