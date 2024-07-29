using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductBrandModel
    {
        [Key]
        public int productbrandId { get; set; }
        [Required]
        [MaxLength(200)]
        public string productbrandName { get; set; }
        [Required]
        [MaxLength(200)]
        public string productbrandphoneNo { get; set; }
        [Required]
        [MaxLength(200)]
        public string title { get; set; }
        [Required]
        [MaxLength(200)]
        public string description { get; set; }
        [Required]
        [MaxLength(200)]
        public string tag { get; set; }
        public int StatusId { get; set; }
    }
}
