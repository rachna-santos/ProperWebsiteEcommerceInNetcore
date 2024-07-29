using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ProductColor
    {
        [key]
        public int productcolorId { get; set; }
        [Required]
        [MaxLength(200)]
        public string productcolorName { get; set; }
        [Required]
        [MaxLength(200)]
        public string colorcode1 { get; set; }
        [Required]
        [MaxLength(200)]
        public string colorcode2 { get; set; }
        public string? imagepath { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }
    }
}
