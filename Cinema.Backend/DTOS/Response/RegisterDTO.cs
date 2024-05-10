using System.ComponentModel.DataAnnotations;

namespace Cinema.Backend.DTOS.Response
{
    public class RegisterDTO
    {
        [Required]
        public string LastName { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; init; } = string.Empty;
        [Required]
        public string Password { get; init; } = string.Empty;

        public string PhoneNumber { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}
