using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Rewies
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string comment { get; set; }
        public int productId { get; set; }
        public virtual Product Product { get; set;}
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }

    }
}


