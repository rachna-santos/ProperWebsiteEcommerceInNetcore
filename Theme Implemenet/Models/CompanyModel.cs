using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class CompanyModel
    {

        [Key]
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "CompanyName is required.")]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "ComapnyContactNo is required.")]
        [MaxLength(200)]
        public string ComapnyContactNo { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200)]
        public string ComapnyEmail { get; set; }
        [Required(ErrorMessage = "CompanyEmail is required.")]
        [MaxLength(200)]
        public string Address { get; set; }
        
 
    }
}
