using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ProductMaterial
    {
        [Key]
        public int productmaterialId { get; set; }
        [Required]
        [MaxLength]
        public string productmaterialname { get; set; }
        [Required]
        [MaxLength]
        public string title { get; set; }
        [Required]
        [MaxLength(200)]
        public string description { get; set; }
        [Required]
        [MaxLength(200)]
        public string tag { get; set; }
        [Required]
        [MaxLength(200)]
        public string Keyword { get; set; }
        public string? imagepath { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }
    }
}
