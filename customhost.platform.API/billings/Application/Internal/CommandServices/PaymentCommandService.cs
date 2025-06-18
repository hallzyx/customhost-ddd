using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.billings.Application.Internal.CommandServices;

/// <summary>
/// Payment Command Service Implementation
/// </summary>
public class PaymentCommandService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork) 
    : IPaymentCommandService
{
    /// <inheritdoc />
    public async Task<Payment?> Handle(CreatePaymentCommand command)
    {
        try
        {
            var payment = new Payment(command);
            await paymentRepository.AddAsync(payment);
            await unitOfWork.CompleteAsync();
            return payment;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Payment?> Handle(UpdatePaymentCommand command)
    {
        try
        {
            var payment = await paymentRepository.FindByIdAsync(command.Id);
            if (payment == null) return null;

            payment.UpdatePayment(command);
            paymentRepository.Update(payment);
            await unitOfWork.CompleteAsync();
            return payment;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeletePaymentCommand command)
    {
        try
        {
            var payment = await paymentRepository.FindByIdAsync(command.Id);
            if (payment == null) return false;

            paymentRepository.Remove(payment);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
