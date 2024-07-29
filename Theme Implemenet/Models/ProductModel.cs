using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ProductModel
    {
        [Required]
        [MaxLength(200)]
        public string productName { get; set; }
        [Required]
        [MaxLength(200)]
        public string productcod { get; set; }  
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
        public int subcategoryId { get; set; }
        public int categorystyleid { get; set; }
        public int productseasonId { get; set; }
        public int productgenderId { get; set; }
        public int productmaterialId { get; set; }
        public int StatusId { get; set; }
        public int price { get; set; }

    }
}
