using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Theme_Implemenet.Models
{
    public class Promotionmapping
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int productId { get; set; }
        public virtual Product Product { get; set;}
        [ForeignKey("Promotions")]
        [Required]
        public int proId { get; set; }
        public virtual Promotions Promotions { get; set;} 

    }
}

