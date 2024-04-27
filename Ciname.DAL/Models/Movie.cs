using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("movies")]
    public class Movie
    {

        [Key]
        [Column("movie_id")]
        public int Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]    
        public Category Category { get; set; }
        

    }
}
