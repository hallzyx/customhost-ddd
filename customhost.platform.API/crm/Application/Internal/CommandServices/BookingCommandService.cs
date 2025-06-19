using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

/// <summary>
/// Booking command service implementation
/// </summary>
public class BookingCommandService(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork) 
    : IBookingCommandService
{
    /// <inheritdoc />
    public async Task<Booking?> Handle(CreateBookingCommand command)
    {
        // Validate command
        command.Validate();
        
        // Check if room is available for the given date range
        if (!await bookingRepository.IsRoomAvailableAsync(command.RoomId, command.CheckInDate, command.CheckOutDate))
        {
            throw new InvalidOperationException($"Room {command.RoomId} is not available from {command.CheckInDate:yyyy-MM-dd} to {command.CheckOutDate:yyyy-MM-dd}");
        }

        var booking = new Booking(command);
        try
        {
            await bookingRepository.AddAsync(booking);
            await unitOfWork.CompleteAsync();
            return booking;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Booking?> Handle(UpdateBookingCommand command)
    {
        // Validate command
        command.Validate();
        
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return null;

        // Check if room is available for the new date range (excluding current booking)
        if (!await bookingRepository.IsRoomAvailableAsync(booking.RoomId, command.CheckInDate, command.CheckOutDate, booking.Id))
        {
            throw new InvalidOperationException($"Room {booking.RoomId} is not available from {command.CheckInDate:yyyy-MM-dd} to {command.CheckOutDate:yyyy-MM-dd}");
        }

        try
        {
            booking.UpdateBookingInfo(command.CheckInDate, command.CheckOutDate, command.TotalPrice, command.SpecialRequests);
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return booking;
        }
        catch (Exception)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(ConfirmBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            booking.Confirm();
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(CheckInBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            booking.CheckIn();
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(CheckOutBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            booking.CheckOut();
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(CancelBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            booking.Cancel();
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }    /// <inheritdoc />
    public async Task<bool> Handle(MarkBookingAsNoShowCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            booking.MarkAsNoShow();
            bookingRepository.Update(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.Id);
        if (booking == null)
            return false;

        try
        {
            bookingRepository.Remove(booking);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }
}
