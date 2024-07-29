
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ProductVeriation
    {
        [Key]
        public int veriationId { get; set; }
        public string veriationName { get; set; }
        public int costprice { get; set; }
        public int RetailerPrice { get; set; }
        public int Quantity { get; set; }
        public string? image { get; set; }
        public int productId { get; set; }
        public virtual Product Product { get; set; }
        public int productcolorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public int productsizeId { get; set; }
        public virtual ProductSize ProductSize { get; set; }
        public int categoryId { get; set; }
        public virtual Category Category { get; set; }
        public int subcategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int categorystyleid { get; set; }
        public virtual CategoryStyle CategoryStyle{ get; set; }
        public int productseasonId { get; set; }
        public virtual ProductSeason ProductSeason { get; set; }
        public int productgenderId { get; set; }
        public virtual Productgender Productgender { get; set; }
        public int productmaterialId { get; set; }
        public virtual ProductMaterial ProductMaterial { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        [NotMapped]
        public IFormFile profilepicture { get; set; }
    }
}
