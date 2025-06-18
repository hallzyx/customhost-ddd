using customhost_backend.crm.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{    public static void ApplyCrmConfiguration(this ModelBuilder builder)
    {        // Hotel
        builder.Entity<Hotel>().HasKey(h => h.Id);
        builder.Entity<Hotel>().Property(h => h.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Hotel>().Property(h => h.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Hotel>().Property(h => h.Address).IsRequired().HasMaxLength(500);
        builder.Entity<Hotel>().Property(h => h.Email).IsRequired().HasMaxLength(255);
        builder.Entity<Hotel>().Property(h => h.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<Hotel>().Property(h => h.Status).IsRequired().HasConversion<string>();
        builder.Entity<Hotel>().Property(h => h.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Entity<Hotel>().Property(h => h.AdminId).IsRequired();
          // Specify table name explicitly
        builder.Entity<Hotel>().ToTable("hotels");
        
        // Booking
        builder.Entity<Booking>().HasKey(b => b.Id);
        builder.Entity<Booking>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Booking>().Property(b => b.UserId).IsRequired();
        builder.Entity<Booking>().Property(b => b.HotelId).IsRequired();
        builder.Entity<Booking>().Property(b => b.RoomId).IsRequired();
        builder.Entity<Booking>().Property(b => b.CheckInDate).IsRequired().HasColumnType("datetime");
        builder.Entity<Booking>().Property(b => b.CheckOutDate).IsRequired().HasColumnType("datetime");
        builder.Entity<Booking>().Property(b => b.Status).IsRequired().HasConversion<string>();
        builder.Entity<Booking>().Property(b => b.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Entity<Booking>().Property(b => b.PaymentStatus).IsRequired().HasConversion<string>();
        builder.Entity<Booking>().Property(b => b.SpecialRequests).HasMaxLength(1000);
        builder.Entity<Booking>().Property(b => b.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Entity<Booking>().Property(b => b.Preferences).HasColumnType("text");
        builder.Entity<Booking>().Property(b => b.AppliedDevicePreferences).HasColumnType("text");
        
        // Specify table name explicitly
        builder.Entity<Booking>().ToTable("bookings");
          // Room
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.RoomNumber).IsRequired();
        builder.Entity<Room>().Property(r => r.Status).IsRequired().HasConversion<string>();
        builder.Entity<Room>().Property(r => r.Type).IsRequired().HasConversion<string>();
        builder.Entity<Room>().Property(r => r.HotelId).IsRequired();
        builder.Entity<Room>().Property(r => r.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Entity<Room>().Property(r => r.Floor).IsRequired();
        // Specify table name explicitly
        builder.Entity<Room>().ToTable("rooms");

        // RoomAudit
        builder.Entity<RoomAudit>().HasKey(nameof(RoomAudit.CreatedDate), nameof(RoomAudit.UpdatedDate));
        builder.Entity<RoomAudit>().Property(a => a.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<RoomAudit>().Property(a => a.UpdatedDate).HasColumnName("UpdatedAt");        // ServiceRequest
        builder.Entity<ServiceRequest>().HasKey(s => s.Id);
        builder.Entity<ServiceRequest>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ServiceRequest>().Property(s => s.UserId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.HotelId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.RoomId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.Type).IsRequired().HasConversion<string>();
        builder.Entity<ServiceRequest>().Property(s => s.Category).IsRequired().HasMaxLength(100);
        builder.Entity<ServiceRequest>().Property(s => s.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<ServiceRequest>().Property(s => s.Status).IsRequired().HasConversion<string>();
        builder.Entity<ServiceRequest>().Property(s => s.Priority).IsRequired().HasConversion<string>();
        builder.Entity<ServiceRequest>().Property(s => s.AssignedTo).IsRequired(false);
        builder.Entity<ServiceRequest>().Property(s => s.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Entity<ServiceRequest>().Property(s => s.CompletedAt).HasColumnType("datetime").IsRequired(false);
        builder.Entity<ServiceRequest>().Property(s => s.History).IsRequired().HasColumnType("text");
        // Specify table name explicitly
        builder.Entity<ServiceRequest>().ToTable("service_requests");// StaffMember
        builder.Entity<StaffMember>().HasKey(s => s.Id);
        builder.Entity<StaffMember>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<StaffMember>().Property(s => s.HotelId).IsRequired();
        builder.Entity<StaffMember>().Property(s => s.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<StaffMember>().Property(s => s.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<StaffMember>().Property(s => s.Email).IsRequired().HasMaxLength(255);
        builder.Entity<StaffMember>().Property(s => s.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<StaffMember>().Property(s => s.Status).IsRequired().HasConversion<string>();
        builder.Entity<StaffMember>().Property(s => s.Department).IsRequired().HasConversion<string>();
        builder.Entity<StaffMember>().Property(s => s.CreatedAt).IsRequired().HasColumnType("datetime");
        // Specify table name explicitly
        builder.Entity<StaffMember>().ToTable("staff_members");

        // Notification
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(n => n.UserId).IsRequired();
        builder.Entity<Notification>().Property(n => n.Title).IsRequired().HasMaxLength(200);
        builder.Entity<Notification>().Property(n => n.Message).IsRequired().HasMaxLength(1000);
        builder.Entity<Notification>().Property(n => n.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Notification>().Property(n => n.Read).IsRequired();
        builder.Entity<Notification>().Property(n => n.CreatedAt).IsRequired().HasColumnType("datetime");
        // Specify table name explicitly
        builder.Entity<Notification>().ToTable("notifications");

    }
}