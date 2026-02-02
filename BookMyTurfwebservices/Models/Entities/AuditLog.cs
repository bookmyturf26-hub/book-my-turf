using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.Entities;

public class AuditLog
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(100)]
    public string TableName { get; set; }

    [Required]
    [StringLength(36)]
    public string RecordId { get; set; }

    [Required]
    [StringLength(20)]
    public string Action { get; set; } // "INSERT", "UPDATE", "DELETE", "SOFT_DELETE", "RESTORE"

    [Required]
    public string OldValues { get; set; }

    [Required]
    public string NewValues { get; set; }

    [Required]
    public string ChangedColumns { get; set; }

    [StringLength(100)]
    public string? UserId { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(45)]
    public string? IpAddress { get; set; }

    [StringLength(500)]
    public string? UserAgent { get; set; }

    [StringLength(1000)]
    public string? RequestPath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}