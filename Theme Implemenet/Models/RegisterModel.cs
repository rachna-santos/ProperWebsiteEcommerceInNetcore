using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class RegisterModel 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public int CompanyId { get; set; }
        
    }
}
