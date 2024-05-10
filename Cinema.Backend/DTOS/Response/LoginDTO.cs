using System.ComponentModel.DataAnnotations;

namespace Cinema.Backend.DTOS.Response
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
