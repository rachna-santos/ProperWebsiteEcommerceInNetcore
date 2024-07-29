using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }  
        public string Type { get; set; }
    }
}
