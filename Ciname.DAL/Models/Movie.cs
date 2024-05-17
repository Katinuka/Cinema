using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Table("movie")]
public class Movie
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("director")]
    public string? Director { get; set; }

    [Column("cast")]
    public string? Cast { get; set; }


    [MaybeNull]
    [Column("genre_id")]
    public int? GenreId { get; set; } 
    [ForeignKey(nameof(GenreId))]  
    public Genre? Genre { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("duration_time")]
    public int DurationTime { get; set; }

    [Column("release_date")]
    public DateTimeOffset ReleaseDate { get; set; }

    [NotMapped]
    public DateTimeOffset ReleaseDateUtc => ReleaseDate.ToUniversalTime();

    [Column("price")]
    public decimal Price { get; set; }

    [Column("now_showing")]
    public bool NowShowing { get; set; }

 
}
