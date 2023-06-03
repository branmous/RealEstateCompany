using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace
    {
        public int Id { get; set; }

        [Display(Name = "Date Sale")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public DateTime DateSale { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Value")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public decimal Value { get; set; }

        [Display(Name = "Tax")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public decimal Tax { get; set; }

        public Property? Property { get; set; }

        public int PropertyId { get; set; }


    }
}
