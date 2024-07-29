using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductGenderModel
    {
        [Required]
        [MaxLength(200)]
        public string productgenderName { get; set; }
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
     
        public int StatusId { get; set; }
    }
}
