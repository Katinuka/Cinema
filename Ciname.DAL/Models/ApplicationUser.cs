using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("application_user")]
    public class ApplicationUser
    {
        [Column("user_id")]
        [Key]
        public int Id { get; set; }
        [Column("last_name")]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("emails")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        [Required]
        public string Password { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = string.Empty;
    }
}
