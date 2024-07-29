using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class Register :ApplicationUser
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
        [DataType(DataType.Password)]
        [MaxLength(200)]
        public string Password { get; set; }
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}



