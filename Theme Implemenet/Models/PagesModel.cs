using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class PagesModel
    {
       
        [Required]
        [MaxLength(200)]
        public string PagesName { get; set; }

        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
      
        public int ModuleId { get; set; }
    }
}
