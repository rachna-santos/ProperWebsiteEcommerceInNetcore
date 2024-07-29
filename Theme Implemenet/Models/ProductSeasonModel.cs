using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductSeasonModel
    {
       
        [Required]
        [MaxLength(200)]
        public string productseasonName { get; set; }
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

    }
}
