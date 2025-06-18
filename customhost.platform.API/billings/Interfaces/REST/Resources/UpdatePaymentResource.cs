namespace customhost_backend.billings.Interfaces.REST.Resources;

public record UpdatePaymentResource
{
    public string? Status { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime? PaymentDate { get; set; }
}
