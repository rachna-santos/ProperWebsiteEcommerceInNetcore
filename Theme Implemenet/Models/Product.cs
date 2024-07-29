using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Product
    {
        [key]
        public int productId { get; set; }
        [Required]
        [MaxLength(200)]
        public string productName { get; set; }
        [Required]
        [MaxLength(200)]
        public string productcod { get; set; }
        public string? productimage { get; set; }
        [Required]
        [MaxLength(200)]
        public string sizeguideimage { get; set; }
        [Required]
        [MaxLength(200)]
        public string title { get; set; }
        [Required]
        [MaxLength(200)]
        public string description { get; set; }
        [Required]
        [MaxLength(200)]
        public string Keyword { get; set; }
        public int categoryId { get; set; }
        public virtual Category Category { get; set; }
        public int subcategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int categorystyleid { get; set; }
        public virtual CategoryStyle CategoryStayle { get; set; }
        public int productseasonId { get; set; }
        public virtual ProductSeason ProductSeason { get; set; }
        public int productgenderId { get; set; }
        public virtual Productgender Productgender { get; set; }
        public int productmaterialId { get; set;}
        public int price { get; set;}
        public int? Discount {get;set;}
        public virtual ProductMaterial ProductMaterial {get; set;}
        public int StatusId { get;set; }
        public virtual Status Status {get;set;}
        public DateTime CreateDate {get; set;}
        public DateTime Lastmodifield { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }

    }

}
