using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class Owner : IdentityUser
    {

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Address")]
        [MaxLength(255, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Photo")]
        public string? Photo { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public DateTime Birthday { get; set; }

        public ICollection<Property>? Properties { get; set; }
    }
}
