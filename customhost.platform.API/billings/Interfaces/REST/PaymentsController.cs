using System.Net.Mime;
using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.billings.Domain.Services;
using customhost_backend.billings.Interfaces.REST.Resources;
using customhost_backend.billings.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.billings.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Payments")]
public class PaymentsController(
    IPaymentCommandService paymentCommandService,
    IPaymentQueryService paymentQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new payment",
        Description = "Creates a new payment with the specified details.",
        OperationId = "CreatePayment")]
    [SwaggerResponse(201, "Payment created successfully", typeof(PaymentResource))]
    [SwaggerResponse(400, "Payment can't be created.", null)]
    public async Task<ActionResult> CreatePayment([FromBody] CreatePaymentResource paymentResource)
    {
        var command = CreatePaymentCommandFromResourceAssembler.ToCommandFromResource(paymentResource);
        var result = await paymentCommandService.Handle(command);
        if (result == null)
            return BadRequest("Payment could not be created.");

        return CreatedAtAction(nameof(GetPaymentById), new { id = result.Id }, 
            PaymentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all payments",
        Description = "Retrieves a list of all payments.",
        OperationId = "GetPayments")]
    [SwaggerResponse(200, "Payments retrieved successfully", typeof(IEnumerable<PaymentResource>))]
    public async Task<ActionResult> GetPayments()
    {
        var payments = (await paymentQueryService.GetAllAsync()).ToList();
        var resources = PaymentResourceFromEntityAssembler.ToResourcesFromEntities(payments);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get payment by ID",
        Description = "Retrieves a specific payment by its ID.",
        OperationId = "GetPaymentById")]
    [SwaggerResponse(200, "Payment retrieved successfully", typeof(PaymentResource))]
    [SwaggerResponse(404, "Payment not found", null)]
    public async Task<ActionResult> GetPaymentById(int id)
    {
        var payment = await paymentQueryService.GetByIdAsync(id);
        if (payment == null)
            return NotFound($"Payment with ID {id} not found.");

        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get payments by user ID",
        Description = "Retrieves all payments for a specific user.",
        OperationId = "GetPaymentsByUserId")]
    [SwaggerResponse(200, "Payments retrieved successfully", typeof(IEnumerable<PaymentResource>))]
    public async Task<ActionResult> GetPaymentsByUserId(int userId)
    {
        var payments = (await paymentQueryService.GetByUserIdAsync(userId)).ToList();
        var resources = PaymentResourceFromEntityAssembler.ToResourcesFromEntities(payments);
        return Ok(resources);
    }

    [HttpGet("hotel/{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Get payments by hotel ID",
        Description = "Retrieves all payments for a specific hotel.",
        OperationId = "GetPaymentsByHotelId")]
    [SwaggerResponse(200, "Payments retrieved successfully", typeof(IEnumerable<PaymentResource>))]
    public async Task<ActionResult> GetPaymentsByHotelId(int hotelId)
    {
        var payments = (await paymentQueryService.GetByHotelIdAsync(hotelId)).ToList();
        var resources = PaymentResourceFromEntityAssembler.ToResourcesFromEntities(payments);
        return Ok(resources);
    }

    [HttpGet("booking/{bookingId:int}")]
    [SwaggerOperation(
        Summary = "Get payment by booking ID",
        Description = "Retrieves the payment for a specific booking.",
        OperationId = "GetPaymentByBookingId")]
    [SwaggerResponse(200, "Payment retrieved successfully", typeof(PaymentResource))]
    [SwaggerResponse(404, "Payment not found", null)]
    public async Task<ActionResult> GetPaymentByBookingId(int bookingId)
    {
        var payment = await paymentQueryService.GetByBookingIdAsync(bookingId);
        if (payment == null)
            return NotFound($"Payment for booking ID {bookingId} not found.");

        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }

    [HttpGet("status/{status}")]
    [SwaggerOperation(
        Summary = "Get payments by status",
        Description = "Retrieves all payments with a specific status.",
        OperationId = "GetPaymentsByStatus")]
    [SwaggerResponse(200, "Payments retrieved successfully", typeof(IEnumerable<PaymentResource>))]
    [SwaggerResponse(400, "Invalid status value", null)]
    public async Task<ActionResult> GetPaymentsByStatus(string status)
    {
        if (!Enum.TryParse<EPaymentStatus>(status, true, out var statusEnum))
            return BadRequest($"Invalid status value: {status}");

        var payments = (await paymentQueryService.GetByStatusAsync(statusEnum)).ToList();
        var resources = PaymentResourceFromEntityAssembler.ToResourcesFromEntities(payments);
        return Ok(resources);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a payment",
        Description = "Updates an existing payment with new information.",
        OperationId = "UpdatePayment")]
    [SwaggerResponse(200, "Payment updated successfully", typeof(PaymentResource))]
    [SwaggerResponse(404, "Payment not found", null)]
    [SwaggerResponse(400, "Payment update failed", null)]
    public async Task<ActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentResource updateResource)
    {
        var command = UpdatePaymentCommandFromResourceAssembler.ToCommandFromResource(id, updateResource);
        var result = await paymentCommandService.Handle(command);
        if (result == null)
            return NotFound($"Payment with ID {id} not found.");

        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete payment",
        Description = "Deletes a payment from the system.",
        OperationId = "DeletePayment")]
    [SwaggerResponse(200, "Payment deleted successfully")]
    [SwaggerResponse(404, "Payment not found", null)]
    [SwaggerResponse(400, "Payment deletion failed", null)]
    public async Task<ActionResult> DeletePayment(int id)
    {
        var command = new DeletePaymentCommand(id);
        var result = await paymentCommandService.Handle(command);
        if (!result)
            return NotFound($"Payment with ID {id} not found.");

        return Ok($"Payment with ID {id} deleted successfully.");
    }
}
