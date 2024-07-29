using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class CategoryModel
    {
        [Key]
        public int categoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string categoryName { get; set; }
        [Required]
    
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
    }
}
