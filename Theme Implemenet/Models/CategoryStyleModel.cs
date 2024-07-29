using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class CategoryStyleModel
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
        public int categoryId { get; set; }
        public int subcategoryId { get; set; }
    }
}
