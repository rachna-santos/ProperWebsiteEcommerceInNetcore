using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class Pages
    {
        public int PagesId { get; set; }
        [Required]
        [MaxLength(200)]
        public string PagesName { get; set; }
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public virtual Module Module { get; set; }
        public int ModuleId { get; set; }
    }
}
