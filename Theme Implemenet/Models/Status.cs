using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ApplicationRole> Role { get; set; }

    }
}
