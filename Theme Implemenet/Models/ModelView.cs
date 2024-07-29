using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Theme_Implemenet.Models
{
    public class ModelView
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public IEnumerable<string> Role { get; set; }
    }
}
