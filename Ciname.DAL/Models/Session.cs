using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("session")]
public class Session
{
    [Key]
    [Column("session")]
    public int SessionId { get; set; }


    [Column("movie_id")]
    public int? MovieId { get; set; }
    [ForeignKey(nameof(MovieId))]
    public Movie Movie { get; set; }


    [Column("room_id")]
    public int? CinemaRoomId { get; set; }  
    [ForeignKey(nameof(CinemaRoomId))]
    public CinemaRoom CinemaRoom { get; set; }

    [Column("session_time")]
    public double? SessionTime { get; set; }
 

  
}
