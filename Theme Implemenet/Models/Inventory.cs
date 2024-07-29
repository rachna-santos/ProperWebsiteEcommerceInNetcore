using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Inventory
    {
        [key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order_Tbl {get; set;}
        [ForeignKey("ProductVeriation")]
        public int veriationId { get; set; }
        public virtual ProductVeriation ProductVeriation { get; set; }
        public int quantity { get; set;}
        public int Inqty { get; set; }
        public int totalqty { get; set; }
        public decimal totalamount { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status {get;set;}
        public DateTime CreateDate {get; set;}
        public DateTime Lastmodifield {get; set;}
    }
}



