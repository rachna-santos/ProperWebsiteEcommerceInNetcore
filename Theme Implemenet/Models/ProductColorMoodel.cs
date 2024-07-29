using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductColorMoodel
    {
        [Required]
        [MaxLength(200)]
        public string productcolorName { get; set; }
        [Required]
        [MaxLength(200)]
        public string colorcode1 { get; set; }
        [Required]
        [MaxLength(200)]
        public string colorcode2 { get; set; }
        public int StatusId { get; set; }
    }
}
