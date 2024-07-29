using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Payment
    {
        [key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order {get; set;}
        [ForeignKey("Customer")]
        public int CustId { get; set; }
        public virtual Customer Customer {get; set;}
        public int PaymentTypeId { get; set;}    
        public virtual PaymentType PaymentType { get; set;}
        public  decimal totalamount {get; set;}
        public string PaymentStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Lastmodifield { get; set;}

    }

}




