using Cinema.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Table("reservation")]
public class Reservation
{
    [Key]
    [Column("reservation_id")]
    public int ReservationId { get; set; }


    [MaybeNull]
    [Column("session_id")]
    public int? SessionId { get; set; }
    [ForeignKey(nameof(SessionId))]
    public Session? Session { get; set; }

    [MaybeNull]
    [Column("user_id")]
    public int? ApplicationUserId { get; set; } 
    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? ApplicationUser { get; set; }

    [Column("total_sum")]
    public decimal? TotalSum { get; set; }

    [Column("reserved")]
    public bool Reserved { get; set; } = false;

    [Column("paid")]
    public bool IsPaid { get; set; } = false;

    [Column("active")]
    public bool IsActive { get; set; } = false;

 
}
