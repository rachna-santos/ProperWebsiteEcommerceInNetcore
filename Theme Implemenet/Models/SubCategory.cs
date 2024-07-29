using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class SubCategory
    {
        [Key]
        public int subcategoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string subcategoryName { get; set; }
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
        public int categoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public string? imagepath { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }
    }
}
