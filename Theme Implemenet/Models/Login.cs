using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Theme_Implemenet.Models
{
   
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember Me")]
        ////public bool RememberMe { get; set; }
        //public string Returnurl { get; set; }

    }
    
}
