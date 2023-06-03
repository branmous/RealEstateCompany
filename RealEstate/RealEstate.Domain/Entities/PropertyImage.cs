using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        public int Id { get; set; }

        [Display(Name = "File")]
        [Required(ErrorMessage = "Field {0} is required.")]
        public string? File { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        public Property? Property { get; set; }

        public int PropertyId { get; set; }
    }
}
