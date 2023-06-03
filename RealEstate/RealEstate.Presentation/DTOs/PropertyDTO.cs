using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.DTOs
{
    public class PropertyDTO
    {
        [Display(Name = "Property Name")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Property Address")]
        [MaxLength(255, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Property Price")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public decimal Price { get; set; }

        [Display(Name = "Property Code Internal")]
        [MaxLength(255, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string CodeInternal { get; set; } = null!;

        [Display(Name = "Property Year")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public int Year { get; set; }
    }
}
