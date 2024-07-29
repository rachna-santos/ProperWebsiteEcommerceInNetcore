using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductSize
    {
        [key]
        public int productsizeId { get; set; }
        [Required]
        [MaxLength]
        public string productsizeName { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
    }
}

