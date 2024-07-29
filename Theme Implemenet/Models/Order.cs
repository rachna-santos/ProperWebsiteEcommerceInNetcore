using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set;}
        [ForeignKey("ProductVeriation")]
        public int veriationId { get; set;}
        public virtual ProductVeriation ProductVeriation { get; set; }
        public int quantity { get; set; }
        [ForeignKey("Customer")]
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal subtotal { get; set; }   
        public decimal shipping { get; set; }
        public decimal totalamount { get; set; }
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set;}

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Lastmodifield { get; set;}
    }
}




