using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Theme_Implemenet.Models
{
    public class contact
    {
        [Key]
        public int contactId { get; set; }

        [ForeignKey("Signup")]
        public int Id { get; set; }
        public virtual Signup Signup { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        

    }
}
