
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("application_user")]
    public class ApplicationUser : IdentityUser
    {
        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }
    }
}
