using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Booking Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Booking aggregate root.
/// It contains the properties and methods to manage booking information.
/// </remarks>
public class Booking
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int HotelId { get; private set; }
    public int RoomId { get; private set; }
    public DateTime CheckInDate { get; private set; }    public DateTime CheckOutDate { get; private set; }
    public BookingStatus Status { get; private set; }
    public decimal TotalPrice { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public string? SpecialRequests { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Preferences { get; private set; } // JSON string for flexibility
    public string AppliedDevicePreferences { get; private set; } // JSON string for device preferences
    
    public bool IsActive => Status != BookingStatus.Cancelled && Status != BookingStatus.NoShow;
    public bool IsCurrentlyCheckedIn => Status == BookingStatus.CheckedIn;
    public int NumberOfNights => (CheckOutDate.Date - CheckInDate.Date).Days;
      // For EF Core
    protected Booking() 
    {
        SpecialRequests = null;
        Preferences = "{}";
        AppliedDevicePreferences = "[]";
        Status = BookingStatus.Pending;
        PaymentStatus = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Booking(int userId, int hotelId, int roomId, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice, string? specialRequests = null)
    {
        ValidateDates(checkInDate, checkOutDate);
        
        UserId = userId;
        HotelId = hotelId;
        RoomId = roomId;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        TotalPrice = totalPrice;        Status = BookingStatus.Pending;
        PaymentStatus = PaymentStatus.Pending;
        SpecialRequests = specialRequests;
        CreatedAt = DateTime.UtcNow;
        Preferences = "{}";
        AppliedDevicePreferences = "[]";
    }

    public Booking(CreateBookingCommand command)
    {
        ValidateDates(command.CheckInDate, command.CheckOutDate);
        
        UserId = command.UserId;
        HotelId = command.HotelId;        RoomId = command.RoomId;
        CheckInDate = command.CheckInDate;
        CheckOutDate = command.CheckOutDate;
        TotalPrice = command.TotalPrice;
        Status = BookingStatus.Pending;
        PaymentStatus = PaymentStatus.Pending;
        SpecialRequests = command.SpecialRequests;
        CreatedAt = DateTime.UtcNow;
        Preferences = command.Preferences ?? "{}";
        AppliedDevicePreferences = command.AppliedDevicePreferences ?? "[]";
    }
    
    public void UpdateBookingInfo(DateTime checkInDate, DateTime checkOutDate, decimal totalPrice, string? specialRequests = null)
    {
        if (Status == BookingStatus.CheckedIn || Status == BookingStatus.CheckedOut)
            throw new InvalidOperationException("Cannot update booking that is already checked in or checked out");
            
        ValidateDates(checkInDate, checkOutDate);
        
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;        TotalPrice = totalPrice;
        SpecialRequests = specialRequests;
    }
    
    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm booking with status {Status}");
            
        Status = BookingStatus.Confirmed;
    }
    
    public void CheckIn()
    {
        if (Status != BookingStatus.Confirmed)
            throw new InvalidOperationException($"Cannot check in booking with status {Status}");
            
        if (DateTime.Now.Date < CheckInDate.Date)
            throw new InvalidOperationException("Cannot check in before check-in date");
            
        Status = BookingStatus.CheckedIn;
    }
    
    public void CheckOut()
    {
        if (Status != BookingStatus.CheckedIn)
            throw new InvalidOperationException($"Cannot check out booking with status {Status}");
            
        Status = BookingStatus.CheckedOut;
    }
    
    public void Cancel()
    {
        if (Status == BookingStatus.CheckedIn || Status == BookingStatus.CheckedOut)
            throw new InvalidOperationException("Cannot cancel booking that is already checked in or checked out");
            
        Status = BookingStatus.Cancelled;
    }
    
    public void MarkAsNoShow()
    {
        if (Status != BookingStatus.Confirmed)
            throw new InvalidOperationException($"Cannot mark as no-show booking with status {Status}");
            
        Status = BookingStatus.NoShow;
    }
    
    public void UpdatePaymentStatus(PaymentStatus paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }
    
    public void UpdatePreferences(string preferences, string appliedDevicePreferences)
    {
        Preferences = preferences ?? "{}";
        AppliedDevicePreferences = appliedDevicePreferences ?? "[]";
    }
    
    private static void ValidateDates(DateTime checkInDate, DateTime checkOutDate)
    {
        if (checkInDate >= checkOutDate)
            throw new ArgumentException("Check-in date must be before check-out date");
            
        if (checkInDate.Date < DateTime.Now.Date)
            throw new ArgumentException("Check-in date cannot be in the past");
    }
}
