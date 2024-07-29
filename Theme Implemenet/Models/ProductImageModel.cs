using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ProductImageModel
    {
        
        public int productId { get; set; }
        public string iamgepath { get; set; }
        public string imagepath_thumb { get; set; }
        public int StatusId { get; set; }
 
    }
}
