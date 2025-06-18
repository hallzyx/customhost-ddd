using customhost_backend.billings.Domain.Models.ValueObjects;

namespace customhost_backend.billings.Domain.Models.Commands;

public record UpdatePaymentCommand(
    int Id,
    EPaymentStatus? Status,
    EPaymentMethod? PaymentMethod,
    DateTime? PaymentDate
);
