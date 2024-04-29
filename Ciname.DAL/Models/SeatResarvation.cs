using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cinema.DAL.Models
{
    [Table("seat_reservation")]
    public class SeatReservation
    {
        [Key]
        [Column("seat_reservation")]
        public int Id { get; set; }

        [Column("number_of_seat")]
        public int? NumberOfSeat { get; set; }

        [Column("reservation_id")]
        public int? ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation Reservation { get; set; }

        [Column("session_id")]
        public int? SessionId { get; set; }
        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }


    }

}

