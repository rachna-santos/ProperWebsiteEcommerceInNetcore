using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Theme_Implemenet.Models
{
    public class userproductpermission
    {
        [Key]
        public int userproductid { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
      
    }
}




