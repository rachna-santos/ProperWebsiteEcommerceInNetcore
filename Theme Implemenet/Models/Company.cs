using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "CompanyName is required.")]
        [MaxLength(200)]
        public string CompanyName { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "ComapnyContactNo is required.")]
        public string ComapnyContactNo { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required(ErrorMessage = "state is required.")]
        [MaxLength(200)]
        public string CompanyEmail { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; }
    }
}



