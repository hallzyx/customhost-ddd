using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.billings.Interfaces.REST.Resources;

namespace customhost_backend.billings.Interfaces.REST.Transform;

public static class UpdatePaymentCommandFromResourceAssembler
{
    public static UpdatePaymentCommand ToCommandFromResource(int id, UpdatePaymentResource resource)
    {
        EPaymentStatus? status = null;
        if (!string.IsNullOrEmpty(resource.Status) && Enum.TryParse<EPaymentStatus>(resource.Status, true, out var parsedStatus))
            status = parsedStatus;

        EPaymentMethod? paymentMethod = null;
        if (!string.IsNullOrEmpty(resource.PaymentMethod) && Enum.TryParse<EPaymentMethod>(resource.PaymentMethod, true, out var parsedPaymentMethod))
            paymentMethod = parsedPaymentMethod;

        return new UpdatePaymentCommand(
            id,
            status,
            paymentMethod,
            resource.PaymentDate
        );
    }
}
