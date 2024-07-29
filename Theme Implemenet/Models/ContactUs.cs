using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ContactUs
    {
        [Key]
        public int contactId { get; set; }
        public string Description {get; set;} 
        public int OrderId {get; set;}
        public virtual Order Order {get; set;}
    }
}



