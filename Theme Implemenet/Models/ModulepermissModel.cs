using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Theme_Implemenet.Models
{
    public class ModulepermissModel
    {
        [Key]
        public int modulepagespermissionId { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public int PagesId { get; set; }
        public int ModuleId { get; set; }
        public int StatusId { get; set; }
    }
}
