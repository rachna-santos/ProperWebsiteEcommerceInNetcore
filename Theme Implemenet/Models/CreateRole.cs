using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class CreateRole 

    {
        
        public string RoleId { get; set;}
        public string RoleName { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public ICollection<Modulepagespermissions> Modulepagespermissions { get; set; }
        public ICollection<userproductpermission> userproductpermissions { get; set; }

        //public ICollection<ApplicationRole> Roles { get; set; }

        //public ICollection<userproductpermission> RolePermissions { get; set; }
        //public ICollection<Modulepagespermissions> role { get; set; }

    }
}

