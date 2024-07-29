using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class VariationClass
    {
        [key]
        public int veriationId { get; set; }
        public int[] costprice { get; set; }
        public int[] RetailerPrice {get; set;}
        public int[] Quantity { get; set;}
        public int[] productId { get; set;}
        public int[] StatusId { get; set; }
        public int[] productcolorId { get; set;}
        public int[] productsizeId { get; set;}
        public string[] colorcode1 { get; set;}
        public string[] colorcode2 { get; set;}
        public string?[] imagepath { get; set;}
    }
}
