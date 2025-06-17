using customhost_backend.crm.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyCrmConfiguration(this ModelBuilder builder)
    {
        // Room
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.RoomNumber).IsRequired();
        builder.Entity<Room>().Property(r => r.Status).IsRequired().HasConversion<string>();
        builder.Entity<Room>().Property(r => r.Type).IsRequired().HasConversion<string>();
        builder.Entity<Room>().Property(r => r.HotelId).IsRequired();

        // RoomAudit
        builder.Entity<RoomAudit>().HasKey(nameof(RoomAudit.CreatedDate), nameof(RoomAudit.UpdatedDate));
        builder.Entity<RoomAudit>().Property(a => a.CreatedDate).HasColumnName("CreatedAt");
        builder.Entity<RoomAudit>().Property(a => a.UpdatedDate).HasColumnName("UpdatedAt");

        // ServiceRequest
        builder.Entity<ServiceRequest>().HasKey(s => s.Id);
        builder.Entity<ServiceRequest>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ServiceRequest>().Property(s => s.UserId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.HotelId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.RoomId).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.Type).IsRequired().HasConversion<string>(); // Enum como string, opcional
        builder.Entity<ServiceRequest>().Property(s => s.Description).IsRequired().HasMaxLength(500);
        builder.Entity<ServiceRequest>().Property(s => s.Status).IsRequired().HasConversion<string>(); // Enum como string, opcional
        builder.Entity<ServiceRequest>().Property(s => s.AsignedTo).IsRequired();
        builder.Entity<ServiceRequest>().Property(s => s.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Entity<ServiceRequest>().Property(s => s.CompleteAt).HasColumnType("datetime").IsRequired(false);
        
        // StaffMember
        builder.Entity<StaffMember>().HasKey(s => s.Id);
        builder.Entity<StaffMember>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<StaffMember>().Property(s => s.HotelId).IsRequired();
        builder.Entity<StaffMember>().Property(s => s.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<StaffMember>().Property(s => s.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<StaffMember>().Property(s => s.Email).IsRequired().HasMaxLength(150);
        builder.Entity<StaffMember>().Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Entity<StaffMember>().Property(s => s.Department).IsRequired().HasMaxLength(100);
        builder.Entity<StaffMember>().Property(s => s.CreatedAt).IsRequired().HasColumnType("datetime");

    }
}