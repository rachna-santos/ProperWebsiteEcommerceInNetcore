using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class CategoryStyle
    {
        [Key]
        public int categorystyleid { get; set; }
        [Required]
        [MaxLength]
        public string categorystyleName { get; set; }
        [Required]
        [MaxLength(200)]
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
        public virtual Category Category { get; set; }
        public int categoryId { get; set; }
        public int subcategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }
 
    }
}
