using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ModuleModel
    {
     
        [Required]
        [MaxLength(200)]
        public string ModuleName { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
    }
}
