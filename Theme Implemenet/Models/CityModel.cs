using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class CityModel
    {
       
        [Required]
        [MaxLength(200)]
        public string cityName { get; set; }
        [Required]
        public int countryId { get; set; }
      
    }
}
