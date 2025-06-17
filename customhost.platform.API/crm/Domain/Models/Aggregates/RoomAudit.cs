using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace customhost_backend.crm.Domain.Models.Aggregates;

public partial class RoomAudit : IEntityWithCreatedUpdatedDate
{
    
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    
    
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}