using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Address")]
        [MaxLength(255, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Photo")]
        public IFormFile? Photo { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public DateTime Birthday { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Password and password confirmation are not the same.")]
        [Display(Name = "Password Confirm")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
