using System.ComponentModel.DataAnnotations;

namespace RealEstate.Presentation.DTOs
{
    public class PropertyPriceDTO
    {
        [Display(Name = "Property Price")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public decimal Price { get; set; }
    }
}
