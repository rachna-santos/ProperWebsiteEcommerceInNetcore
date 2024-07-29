using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class OderStatus
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}

