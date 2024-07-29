using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Theme_Implemenet.Models
{
    public class Signup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set;}
        [Required]
        [MaxLength(200)]
        public string Name { get; set;}
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(200)]
        public string Password {get; set;}
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
    }
}
