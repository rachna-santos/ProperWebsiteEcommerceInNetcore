using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class Modulepagespermissions
    {
        [Key]
        public int modulepagespermissionId { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual Pages Pages { get; set; }
        public int PagesId { get; set; }
        public virtual Module  Module { get; set; }
        public int ModuleId { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }

    }
}

