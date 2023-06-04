using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.DTOs
{
    public class AuthDTO
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Field {0} is required.")]
        [MinLength(6, ErrorMessage = "Field {0} must be at least {1} characters.")]
        public string Password { get; set; } = null!;
    }
}
