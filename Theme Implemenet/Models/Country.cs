using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class Country
    {
        [Key]
        public int countryId { get; set;}
        [Required]
        [MaxLength(200)]
        public string countryName { get; set; }
    }
}
