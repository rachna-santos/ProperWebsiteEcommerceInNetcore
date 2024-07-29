using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Promotions
    {
        [Key]
        public int proId { get; set; }
        [Required]
        public string ProName { get; set; }
        [Required]
        public int Value { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }
}
