﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Theme_Implemenet.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public ICollection<ShoppingCarts> ShoppingCarts { get; set; }
        public ICollection<Rewies> Rewies { get; set; }

        //public ICollection<userproductpermission> userproductpermissions { get; set; }
    }
}