using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class veriationModel
    {
        [Key]
        public int veriationId { get; set; }
        public string veriationName { get; set; }
        public int costprice { get; set; }
        public int RetailerPrice { get; set; }
        public int Quantity { get; set; }
        public int productId { get; set; }
        public int productcolorId { get; set; }
        public int productsizeId { get; set; }
        public int categoryId { get; set; }
        public int subcategoryId { get; set; }
        public int categorystyleid { get; set; }
        public int productseasonId { get; set; }
        public int productgenderId { get; set; }
        public int productmaterialId {get; set;}
        public int StatusId {get; set;}

    }
}
