using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Booking Endpoints.")]

public class BookingController(
    IBookingCommandService bookingCommandService,
    IBookingQueryService bookingQueryService)
: ControllerBase
{
    [HttpGet("{bookingId:int}")]
    [SwaggerOperation("Get Booking by Id", "Get a booking by its unique identifier.", OperationId = "GetBookingById")]
    [SwaggerResponse(200, "The booking was found and returned.", typeof(BookingResource))]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var getBookingByIdQuery = new GetBookingByIdQuery(bookingId);
        var booking = await bookingQueryService.Handle(getBookingByIdQuery);
        if (booking is null) return NotFound();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return Ok(bookingResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Booking", "Create a new booking.", OperationId = "CreateBooking")]
    [SwaggerResponse(201, "The booking was created.", typeof(BookingResource))]
    [SwaggerResponse(400, "The booking was not created.")]
    public async Task<IActionResult> CreateBooking(CreateBookingResource resource)
    {
        var createBookingCommand = CreateBookingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var booking = await bookingCommandService.Handle(createBookingCommand);
        if (booking is null) return BadRequest();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return CreatedAtAction(nameof(GetBookingById), new { bookingId = booking.Id }, bookingResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Bookings", "Get all bookings.", OperationId = "GetAllBookings")]
    [SwaggerResponse(200, "The bookings were found and returned.", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetAllBookings()
    {
        var getAllBookingsQuery = new GetAllBookingsQuery();
        var bookings = await bookingQueryService.Handle(getAllBookingsQuery);
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }

    [HttpPut("{bookingId:int}")]
    [SwaggerOperation("Update Booking", "Update an existing booking.", OperationId = "UpdateBooking")]
    [SwaggerResponse(200, "The booking was updated.", typeof(BookingResource))]
    [SwaggerResponse(404, "The booking was not found.")]
    [SwaggerResponse(400, "The booking was not updated.")]
    public async Task<IActionResult> UpdateBooking(int bookingId, UpdateBookingResource resource)
    {
        var updateBookingCommand = UpdateBookingCommandFromResourceAssembler.ToCommandFromResource(bookingId, resource);
        var booking = await bookingCommandService.Handle(updateBookingCommand);
        if (booking is null) return NotFound();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return Ok(bookingResource);
    }

    [HttpPost("{bookingId:int}/confirm")]
    [SwaggerOperation("Confirm Booking", "Confirm a booking.", OperationId = "ConfirmBooking")]
    [SwaggerResponse(200, "The booking was confirmed.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> ConfirmBooking(int bookingId)
    {
        var confirmBookingCommand = new ConfirmBookingCommand(bookingId);
        var result = await bookingCommandService.Handle(confirmBookingCommand);
        if (!result) return NotFound();
        return Ok("Booking confirmed successfully");
    }

    [HttpPost("{bookingId:int}/checkin")]
    [SwaggerOperation("Check In Booking", "Check in a booking.", OperationId = "CheckInBooking")]
    [SwaggerResponse(200, "The booking was checked in.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> CheckInBooking(int bookingId)
    {
        var checkInBookingCommand = new CheckInBookingCommand(bookingId);
        var result = await bookingCommandService.Handle(checkInBookingCommand);
        if (!result) return NotFound();
        return Ok("Booking checked in successfully");
    }

    [HttpPost("{bookingId:int}/checkout")]
    [SwaggerOperation("Check Out Booking", "Check out a booking.", OperationId = "CheckOutBooking")]
    [SwaggerResponse(200, "The booking was checked out.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> CheckOutBooking(int bookingId)
    {
        var checkOutBookingCommand = new CheckOutBookingCommand(bookingId);
        var result = await bookingCommandService.Handle(checkOutBookingCommand);
        if (!result) return NotFound();
        return Ok("Booking checked out successfully");
    }

    [HttpPost("{bookingId:int}/cancel")]
    [SwaggerOperation("Cancel Booking", "Cancel a booking.", OperationId = "CancelBooking")]
    [SwaggerResponse(200, "The booking was cancelled.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        var cancelBookingCommand = new CancelBookingCommand(bookingId);
        var result = await bookingCommandService.Handle(cancelBookingCommand);
        if (!result) return NotFound();
        return Ok("Booking cancelled successfully");
    }

    [HttpPost("{bookingId:int}/no-show")]
    [SwaggerOperation("Mark Booking as No Show", "Mark a booking as no show.", OperationId = "MarkBookingAsNoShow")]
    [SwaggerResponse(200, "The booking was marked as no show.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> MarkBookingAsNoShow(int bookingId)
    {
        var markAsNoShowCommand = new MarkBookingAsNoShowCommand(bookingId);
        var result = await bookingCommandService.Handle(markAsNoShowCommand);
        if (!result) return NotFound();
        return Ok("Booking marked as no show successfully");
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation("Get Bookings by User", "Get bookings for a specific user.", OperationId = "GetBookingsByUser")]
    [SwaggerResponse(200, "The bookings were found and returned.", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetBookingsByUser(int userId)
    {
        var getBookingsByUserQuery = new GetBookingsByUserIdQuery(userId);
        var bookings = await bookingQueryService.Handle(getBookingsByUserQuery);
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }

    [HttpGet("hotel/{hotelId:int}")]
    [SwaggerOperation("Get Bookings by Hotel", "Get bookings for a specific hotel.", OperationId = "GetBookingsByHotel")]
    [SwaggerResponse(200, "The bookings were found and returned.", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetBookingsByHotel(int hotelId)
    {
        var getBookingsByHotelQuery = new GetBookingsByHotelIdQuery(hotelId);
        var bookings = await bookingQueryService.Handle(getBookingsByHotelQuery);
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }

    [HttpGet("room/{roomId:int}")]
    [SwaggerOperation("Get Bookings by Room", "Get bookings for a specific room.", OperationId = "GetBookingsByRoom")]
    [SwaggerResponse(200, "The bookings were found and returned.", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetBookingsByRoom(int roomId)
    {
        var getBookingsByRoomQuery = new GetBookingsByRoomIdQuery(roomId);
        var bookings = await bookingQueryService.Handle(getBookingsByRoomQuery);
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }    [HttpGet("status/{status}")]
    [SwaggerOperation("Get Bookings by Status", "Get bookings with a specific status.", OperationId = "GetBookingsByStatus")]
    [SwaggerResponse(200, "The bookings were found and returned.", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetBookingsByStatus(string status)
    {
        var getBookingsByStatusQuery = new GetBookingsByStatusQuery(status);
        var bookings = await bookingQueryService.Handle(getBookingsByStatusQuery);
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }

    [HttpDelete("{bookingId:int}")]
    [SwaggerOperation("Delete Booking", "Delete a booking permanently.", OperationId = "DeleteBooking")]
    [SwaggerResponse(204, "The booking was deleted successfully.")]
    [SwaggerResponse(404, "The booking was not found.")]
    public async Task<IActionResult> DeleteBooking(int bookingId)
    {
        var deleteBookingCommand = new DeleteBookingCommand(bookingId);
        var result = await bookingCommandService.Handle(deleteBookingCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
